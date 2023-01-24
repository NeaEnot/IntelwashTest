using Core.Models;
using Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System;
using System.Linq;

namespace CrudApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly MemoryContext _dbContext;

        public ProductController(MemoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public void Create(Product model)
        {
            _dbContext.Products.Add(model);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Product Read(int id)
        {
            Product model = _dbContext.Products.FirstOrDefault(req => req.Id == id);

            return model;
        }

        [HttpGet]
        public IEnumerable<Product> Read()
        {
            return _dbContext.Products.ToArray();
        }

        [HttpPut]
        public void Update(Product model)
        {
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
            Product product = _dbContext.Products.FirstOrDefault(req => req.Id == id);

            if (product == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _dbContext.Products.Remove(product);

            _dbContext.SaveChanges();
        }
    }
}
