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
    public class SalesPointController : Controller
    {
        private readonly MemoryContext _dbContext;
        private readonly ILogger _logger;

        public SalesPointController(MemoryContext dbContext, ILogger<SalesPointController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpPost]
        public void Create(SalesPoint model)
        {
            _logger.Log(LogLevel.Information, "Create SalesPoint");
            _dbContext.SalesPoints.Add(model);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("{id:int}")]
        public SalesPoint Read(int id)
        {
            _logger.Log(LogLevel.Information, "Read SalesPoint");
            SalesPoint model = _dbContext.SalesPoints.FirstOrDefault(req => req.Id == id);

            return model;
        }

        [HttpGet]
        public IEnumerable<SalesPoint> Read()
        {
            _logger.Log(LogLevel.Information, "Read SalesPoints");
            return _dbContext.SalesPoints.ToArray();
        }

        [HttpPut]
        public void Update(SalesPoint model)
        {
            _logger.Log(LogLevel.Information, "Update SalesPoint");
            SalesPoint salesPoint = _dbContext.SalesPoints.FirstOrDefault(req => req.Id == model.Id);

            if (salesPoint == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            salesPoint.ProvidedProducts = model.ProvidedProducts;
            salesPoint.Name = model.Name;

            _dbContext.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _logger.Log(LogLevel.Information, "Delete SalesPoint");
            SalesPoint salesPoint = _dbContext.SalesPoints.FirstOrDefault(req => req.Id == id);

            if (salesPoint == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _dbContext.SalesPoints.Remove(salesPoint);

            _dbContext.SaveChanges();
        }
    }
}
