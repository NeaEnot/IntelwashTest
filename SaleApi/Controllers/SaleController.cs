using Core.Models;
using Core;
using Microsoft.AspNetCore.Mvc;
using SaleApi.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;

namespace SaleApi.Controllers
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
        public string Sale(SaleModel model)
        {
            _logger.Log(LogLevel.Information, "Create Sale");
            SalesPoint salesPoint = _dbContext.SalesPoints.First(req => req.Id == model.SalesPointId);
            List<(SaleData, ProvidedProduct)> changes = new List<(SaleData, ProvidedProduct)>();

            foreach (SaleData saleData in model.SalesData)
            {
                ProvidedProduct providedProduct = salesPoint.ProvidedProducts.FirstOrDefault(req => req.ProductId == saleData.ProductId);

                if (providedProduct == null || providedProduct.ProductQuantity < saleData.ProductQuantity)
                {
                    _logger.Log(LogLevel.Information, "Create Sale failed");
                    return "Нет товаов в необходимом количестве";
                }
                else
                {
                    changes.Add((saleData, providedProduct));
                }
            }

            foreach ((SaleData saleData, ProvidedProduct providedProduct) pair in changes)
            {
                pair.providedProduct.ProductQuantity -= pair.saleData.ProductQuantity;
                pair.saleData.ProductIdAmount = _dbContext.Products.First(req => req.Id == pair.saleData.ProductId).Price * pair.saleData.ProductQuantity;
            }

            Sale sale = new Sale
            {
                BuyerId = model.BuyerId,
                DateTime = DateTime.Now,
                SalesPointId = model.SalesPointId,
                SalesData = model.SalesData,
                TotalAmount = model.SalesData.Sum(req => req.ProductIdAmount)
            };

            _dbContext.Sales.Add(sale);

            if (model.BuyerId != null)
            {
                Buyer buyer = _dbContext.Buyers.First(req => req.Id == model.BuyerId);
                buyer.SalesIds.Add(sale.Id);
            }

            _dbContext.SaveChanges();
            _logger.Log(LogLevel.Information, "Create Sale success");

            return "Успешно";
        }
    }
}
