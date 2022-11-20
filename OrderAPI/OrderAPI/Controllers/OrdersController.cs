using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OrderAPI.Models.DTOs;
using OrderAPI.Models.Entities;
using OrderAPI.Models.Results;
using OrderAPI.OrderDbContext;
using OrderAPI.RaabitMQ;
using System.Net.Mail;
using System.Text;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly OrderApiDbContext _context;
        private readonly IMapper _mapper;
        public OrdersController(IMemoryCache memoryCache, OrderApiDbContext context, IMapper mapper)
        {
            _memoryCache = memoryCache;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(string? category)
        {
            var result = new List<Product>();
            if (category is null)
            {
                result = _memoryCache.Get("products") as List<Product>;
                if (result is null)
                {
                    result = await _context.Products.ToListAsync();
                    _memoryCache.Set("products", result, TimeSpan.FromMinutes(60));
                }

            }
            else
            {
                result = _memoryCache.Get($"products-{category}") as List<Product>;
                if (result is null)
                {
                    result = await _context.Products.Where(x => x.Category == category).ToListAsync();
                    _memoryCache.Set($"products-{category}", result, TimeSpan.FromMinutes(60));
                }

            }

            var productDtos = _mapper.Map<List<Product>, List<ProductDto>>(result);
            return Ok(new ApiResponse<List<ProductDto>>(StatusType.Success, productDtos));
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderRequest createOrderRequest)
        {
            Order order = _mapper.Map<Order>(createOrderRequest);
            List<OrderDetails> orderDetails = _mapper.Map<List<ProductDetailDto>, List<OrderDetails>>(createOrderRequest.ProductDetails) as List<OrderDetails>;
            order.TotalAmount = createOrderRequest.ProductDetails.Sum(p=>p.Amount);
            order.OrderDetails = orderDetails;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            var datas = Encoding.UTF8.GetBytes(createOrderRequest.CustomerEmail);
            SetQueues.SendQueue(datas);
            return Ok(new ApiResponse<int>(StatusType.Success,order.Id));
        }
        //[HttpPost]
        //public async Task<IActionResult> PostAsync()
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        Product product = new()
        //        {
        //            Category = $"Kategori{i}",
        //            CreateDate = DateTime.Now,
        //            Description = "Açıklama",
        //            Status = true,
        //            Unit = i,
        //            UnitPrice = i*10,

        //        };
        //        await _context.Products.AddAsync(product);
        //        await _context.SaveChangesAsync();

        //    }
        //    return Ok("Product oluştu");
        //}
    }
}
