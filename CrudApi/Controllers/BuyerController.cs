using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Core.Models;
using Core;
using System.Net;

namespace CrudApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyerController : Controller
    {
        private readonly MemoryContext _dbContext;

        public BuyerController(MemoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public void Create(Buyer model)
        {
            _dbContext.Buyers.Add(model);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Buyer Read(int id)
        {
            Buyer model = _dbContext.Buyers.FirstOrDefault(req => req.Id == id);

            return model;
        }

        [HttpGet]
        public IEnumerable<Buyer> Read()
        {
            return _dbContext.Buyers.ToArray();
        }

        [HttpPut]
        public void Update(Buyer model)
        {
            Buyer buyer = _dbContext.Buyers.FirstOrDefault(req => req.Id == model.Id);

            if (buyer == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            buyer.SalesIds = model.SalesIds;
            buyer.Name = model.Name;

            _dbContext.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Buyer buyer = _dbContext.Buyers.FirstOrDefault(req => req.Id == id);

            if (buyer == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _dbContext.Buyers.Remove(buyer);

            _dbContext.SaveChanges();
        }
    }
}
