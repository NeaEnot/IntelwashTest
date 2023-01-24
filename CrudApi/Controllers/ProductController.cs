using Core.Models;
using Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace CrudApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly MemoryContext _dbContext;
        private readonly ILogger _logger;

        public ProductController(MemoryContext dbContext, ILogger<ProductController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpPost]
        public void Create(Product model)
        {
            _logger.Log(LogLevel.Information, "Create Product");
            _dbContext.Products.Add(model);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Product Read(int id)
        {
            _logger.Log(LogLevel.Information, "Read Product");
            Product model = _dbContext.Products.FirstOrDefault(req => req.Id == id);

            return model;
        }

        [HttpGet]
        public IEnumerable<Product> Read()
        {
            _logger.Log(LogLevel.Information, "Read Products");
            return _dbContext.Products.ToArray();
        }

        [HttpPut]
        public void Update(Product model)
        {
            _logger.Log(LogLevel.Information, "Update Product");
            Product product = _dbContext.Products.FirstOrDefault(req => req.Id == model.Id);

            if (product == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            product.Price = model.Price;
            product.Name = model.Name;

            _dbContext.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _logger.Log(LogLevel.Information, "Delete Product");
            Product product = _dbContext.Products.FirstOrDefault(req => req.Id == id);

            if (product == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _dbContext.Products.Remove(product);

            _dbContext.SaveChanges();
        }
    }
}
