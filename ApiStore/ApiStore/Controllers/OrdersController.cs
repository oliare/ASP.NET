using ApiStore.Data;
using ApiStore.Data.Entities.Identity;
using ApiStore.Data.Entities.Orders;
using ApiStore.Models.Order;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ApiStoreDbContext _context,
       UserManager<UserEntity> _userManager, IMapper _mapper) : ControllerBase
    {
        [HttpPost("create-basket")]
        public async Task<IActionResult> CreateBasketAsync([FromBody] BasketCreateViewModel model)
        {
            var user = _userManager.FindByEmailAsync(ClaimTypes.Email);

            if (user == null) return NotFound();
            var basket = new BasketEntity
            {
                UserId = user.Id,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                await _context.Baskets.AddAsync(basket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("list-basket")]
        public async Task<IActionResult> GetList()
        {
            var userId = _userManager.FindByEmailAsync(ClaimTypes.Email);
            if (userId == null) return NotFound("User not found");

            var list = await _context.Baskets
               .Where(b => b.UserId == userId.Id)
               .Include(b => b.Product)
               .ToListAsync();

            if (!list.Any()) return NotFound("Basket is empty");

            var basket = _mapper.Map<List<BasketItemViewModel>>(list);
            return Ok(basket);
        }

    }
}
