using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Services;

public class ProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Product>> GetAll()
        => await _db.Products.OrderByDescending(p => p.CriadoEm).ToListAsync();

    public async Task<Product?> GetById(int id)
        => await _db.Products.FindAsync(id);

    public async Task<Product> Create(ProductDTO dto)
    {
        var product = new Product
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Preco = dto.Preco,
            Estoque = dto.Estoque,
            ImagemUrl = dto.ImagemUrl
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> Update(int id, ProductDTO dto)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return null;

        product.Nome = dto.Nome;
        product.Descricao = dto.Descricao;
        product.Preco = dto.Preco;
        product.Estoque = dto.Estoque;
        product.ImagemUrl = dto.ImagemUrl;

        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return false;

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return true;
    }
}