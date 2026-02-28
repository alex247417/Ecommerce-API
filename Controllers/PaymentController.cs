using EcommerceAPI.Data;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommerceAPI.Controllers;

[ApiController]
[Route("api/payment")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;
    private readonly AppDbContext _db;

    public PaymentController(PaymentService paymentService, AppDbContext db)
    {
        _paymentService = paymentService;
        _db = db;
    }

    [HttpPost("checkout/{orderId}")]
    public async Task<IActionResult> CreateCheckout(int orderId)
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var order = await _db.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            if (order.Status != "pendente")
                return BadRequest(new { mensagem = "Pedido já foi pago." });

            var items = order.Items.Select(i => (i.Product.Nome, i.Preco, i.Quantidade)).ToList();

            var successUrl = $"https://ecommerce-front-gules.vercel.app/orders?success=true&orderId={orderId}";
            var cancelUrl = $"https://ecommerce-front-gules.vercel.app/cart?cancelled=true";

            var url = await _paymentService.CreateCheckoutSession(items, successUrl, cancelUrl);

            return Ok(new { url });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensagem = ex.Message, detalhe = ex.InnerException?.Message });
        }
    }
}