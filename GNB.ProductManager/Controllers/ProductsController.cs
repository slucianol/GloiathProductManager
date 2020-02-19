using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GNB.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using GNB.ProductManager.Helpers;
using System;

namespace GNB.ProductManager.Controllers {
    public class ProductsController : Controller {
        private readonly ITransactionService transactionService;
        private readonly IConfiguration configuration;
        public ProductsController(ITransactionService transactionService, IConfiguration configuration) {
            this.transactionService = transactionService;
            this.configuration = configuration;
        }
        [HttpGet("")]
        public IActionResult List(string sku, int? pageNumber) {
            if (sku != null) {
                pageNumber = 1;
            } else {
                ViewData["sku"] = sku;
            }
            
            IQueryable<Models.Transaction> transactions = transactionService
                                                        .GetTransactions()
                                                        .Select(t => new Models.Transaction {
                                                            Sku = t.Sku,
                                                            Amount = t.Amount.ToString(),
                                                            Currency = t.Currency
                                                        });
            if (!string.IsNullOrEmpty(sku)) {
                transactions = transactions.Where(t => t.Sku == ViewData["sku"].ToString());
            }
            ViewData["TotalAmount"] = 0M;//transactions.Sum(t => decimal.Parse(t.ToString()));
            int pageSize = configuration.GetValue<int>("PageSize");
            return View(PaginatedList<Models.Transaction>.CreateAsync(transactions, pageNumber ?? 1, pageSize));
        }
    }
}
