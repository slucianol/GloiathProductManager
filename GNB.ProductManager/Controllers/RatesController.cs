﻿using System.Collections.Generic;
using System.Linq;
using GNB.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using GNB.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GNB.ProductManager.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatesController : ControllerBase {
        private IRatesService ratesService;
        public RatesController(IRatesService ratesService) {
            this.ratesService = ratesService;
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Models.RateConverter>), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public IActionResult Get() {
            return Ok(ratesService.GetRates().Select(r => new Models.RateConverter {
                From = r.From,
                To = r.To,
                Rate = r.Rate.ToString()
            }).ToList()); ;
        }
        [HttpGet("{currency}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Models.RateConverter>), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public IActionResult Get(string currency) {
            return Ok(ratesService.GetRates(currency).Select(r => new Models.RateConverter {
                From = r.From,
                To = r.To,
                Rate = r.Rate.ToString()
            }).ToList());
        }
    }
}