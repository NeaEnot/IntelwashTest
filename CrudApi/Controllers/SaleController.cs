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
    public class SaleController : Controller
    {
        private readonly MemoryContext _dbContext;

        public SaleController(MemoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public void Create(Sale model)
        {
            _dbContext.Sales.Add(model);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Sale Read(int id)
        {
            Sale model = _dbContext.Sales.FirstOrDefault(req => req.Id == id);

            return model;
        }

        [HttpGet]
        public IEnumerable<Sale> Read()
        {
            return _dbContext.Sales.ToArray();
        }

        [HttpPut]
        public void Update(Sale model)
        {
            Sale sale = _dbContext.Sales.FirstOrDefault(req => req.Id == model.Id);

            if (sale == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            sale.SalesData = model.SalesData;
            sale.SalesPointId = model.SalesPointId;
            sale.Time = model.Time;
            sale.Date = model.Date;
            sale.BuyerId = model.BuyerId;
            sale.TotalAmount= model.TotalAmount;

            _dbContext.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Sale sale = _dbContext.Sales.FirstOrDefault(req => req.Id == id);

            if (sale == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _dbContext.Sales.Remove(sale);

            _dbContext.SaveChanges();
        }
    }
}
