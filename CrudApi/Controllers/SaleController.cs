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
    public class SaleController : Controller
    {
        private readonly MemoryContext _dbContext;
        private readonly ILogger _logger;

        public SaleController(MemoryContext dbContext, ILogger<SaleController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpPost]
        public void Create(Sale model)
        {
            _logger.Log(LogLevel.Information, "Create Sale");
            _dbContext.Sales.Add(model);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Sale Read(int id)
        {
            _logger.Log(LogLevel.Information, "Read Sale");
            Sale model = _dbContext.Sales.FirstOrDefault(req => req.Id == id);

            return model;
        }

        [HttpGet]
        public IEnumerable<Sale> Read()
        {
            _logger.Log(LogLevel.Information, "Read Sales");
            return _dbContext.Sales.ToArray();
        }

        [HttpPut]
        public void Update(Sale model)
        {
            _logger.Log(LogLevel.Information, "Update Sale");
            Sale sale = _dbContext.Sales.FirstOrDefault(req => req.Id == model.Id);

            if (sale == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            sale.SalesData = model.SalesData;
            sale.SalesPointId = model.SalesPointId;
            sale.DateTime = model.DateTime;
            sale.BuyerId = model.BuyerId;
            sale.TotalAmount= model.TotalAmount;

            _dbContext.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _logger.Log(LogLevel.Information, "Delete Sale");
            Sale sale = _dbContext.Sales.FirstOrDefault(req => req.Id == id);

            if (sale == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _dbContext.Sales.Remove(sale);

            _dbContext.SaveChanges();
        }
    }
}
