using System.Collections.Generic;
using System.Linq;
using GNB.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using GNB.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GNB.ProductManager.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase {
        private readonly ITransactionService transactionService;
        public TransactionsController(ITransactionService transactionService) {
            this.transactionService = transactionService;
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Transaction>), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public IActionResult Get() {
            return Ok(transactionService.GetTransactions().Select(t => new Models.Transaction {
                Currency = t.Currency,
                Sku = t.Sku,
                Amount = t.Amount.ToString()
            }).ToList());
        }
        [HttpGet("{sku}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Transaction>), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public IActionResult Get(string sku) {
            return Ok(transactionService.GetTransactions(sku).Select(t => new Models.Transaction {
                Currency = t.Currency,
                Sku = t.Sku,
                Amount = t.Amount.ToString()
            }).ToList());
        }
    }
}
