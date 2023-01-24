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
    public class SalesPointController : Controller
    {
        private readonly MemoryContext _dbContext;

        public SalesPointController(MemoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public void Create(SalesPoint model)
        {
            _dbContext.SalesPoints.Add(model);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        [Route("{id:int}")]
        public SalesPoint Read(int id)
        {
            SalesPoint model = _dbContext.SalesPoints.FirstOrDefault(req => req.Id == id);

            return model;
        }

        [HttpGet]
        public IEnumerable<SalesPoint> Read()
        {
            return _dbContext.SalesPoints.ToArray();
        }

        [HttpPut]
        public void Update(SalesPoint model)
        {
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
            SalesPoint salesPoint = _dbContext.SalesPoints.FirstOrDefault(req => req.Id == id);

            if (salesPoint == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _dbContext.SalesPoints.Remove(salesPoint);

            _dbContext.SaveChanges();
        }
    }
}
