using System.Security.Claims;
using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Services;

public class OrderService
{
    private readonly AppDbContext _db;

    public OrderService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Order?> Create(CreateOrderDTO dto, int userId)
    {
        decimal total = 0;
        var items = new List<OrderItem>();

        foreach (var itemDto in dto.Items)
        {
            var product = await _db.Products.FindAsync(itemDto.ProductId);
            if (product == null || product.Estoque < itemDto.Quantidade)
                return null;

            total += product.Preco * itemDto.Quantidade;
            items.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantidade = itemDto.Quantidade,
                Preco = product.Preco
            });

            product.Estoque -= itemDto.Quantidade;
        }

        var order = new Order
        {
            UserId = userId,
            Total = total,
            Items = items
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return order;
    }

    public async Task<List<Order>> GetMyOrders(int userId)
        => await _db.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CriadoEm)
            .ToListAsync();

    public async Task<List<Order>> GetAll()
        => await _db.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Include(o => o.User)
            .OrderByDescending(o => o.CriadoEm)
            .ToListAsync();
}