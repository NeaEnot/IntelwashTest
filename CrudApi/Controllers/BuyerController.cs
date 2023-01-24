using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Core.Models;
using Core;
using System.Net;
using Microsoft.Extensions.Logging;

namespace CrudApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyerController : Controller
    {
        private readonly MemoryContext _dbContext;
        private readonly ILogger _logger;

        public BuyerController(MemoryContext dbContext, ILogger<BuyerController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpPost]
        public void Create(Buyer model)
        {
            _logger.Log(LogLevel.Information, "Create Buyer");
            _dbContext.Buyers.Add(model);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Buyer Read(int id)
        {
            _logger.Log(LogLevel.Information, "Read Buyer");
            Buyer model = _dbContext.Buyers.FirstOrDefault(req => req.Id == id);

            return model;
        }

        [HttpGet]
        public IEnumerable<Buyer> Read()
        {
            _logger.Log(LogLevel.Information, "Read Buyers");
            return _dbContext.Buyers.ToArray();
        }

        [HttpPut]
        public void Update(Buyer model)
        {
            _logger.Log(LogLevel.Information, "Update Buyer");
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
            _logger.Log(LogLevel.Information, "Delete Buyer");
            Buyer buyer = _dbContext.Buyers.FirstOrDefault(req => req.Id == id);

            if (buyer == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _dbContext.Buyers.Remove(buyer);

            _dbContext.SaveChanges();
        }
    }
}
