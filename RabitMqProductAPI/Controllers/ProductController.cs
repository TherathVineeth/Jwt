
using Microsoft.AspNetCore.Mvc;
using RabitMqProductAPI.Models;
using RabitMqProductAPI.RabitMQ;
using RabitMqProductAPI.Services;
namespace RabitMqProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IRabitMQProducer _rabitMQProducer;
        public ProductController(IProductService _productService, IRabitMQProducer rabitMQProducer)
        {
            productService = _productService;
            _rabitMQProducer = rabitMQProducer;
        }
        [HttpGet("productlist")]
        public IEnumerable<Product> ProductList()
        {
            var productList = productService.GetProductList();
            return productList;
        }
        [HttpGet("getproductbyid")]
        public Product GetProductById(int Id)
        {
            return productService.GetProductById(Id);
        }
        [HttpPost("addproduct")]
        public Product AddProduct(Product product)
        {
            var productData = productService.AddProduct(product);
            //send the inserted product data to the queue and consumer will listening this data from queue
            _rabitMQProducer.SendProductMessage(productData);
            return productData;
        }
        [HttpPut("updateproduct")]
        public Product UpdateProduct(Product product)
        {
            return productService.UpdateProduct(product);
        }
        [HttpDelete("deleteproduct")]
        public bool DeleteProduct(int Id)
        {
            return productService.DeleteProduct(Id);
        }
    }
}