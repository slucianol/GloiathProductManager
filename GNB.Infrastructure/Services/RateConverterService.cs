using GNB.Core.Interfaces;
using GNB.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GNB.Infrastructure.Services {
    public class RateConverterService : IRateConverterService {
        private readonly IRatesService ratesService;
        public RateConverterService(IRatesService ratesService) {
            this.ratesService = ratesService;
        }
        public decimal ConvertAmount(string fromCurrency, string toCurrency, decimal amount) {
            IQueryable<RateConverter> rateConverters = ratesService.GetRates();
            decimal? rate = rateConverters.FirstOrDefault(r => r.From == fromCurrency && r.To == toCurrency)?.Rate;
            if (rate.HasValue) {
                return rate.Value * amount;
            }
            List<string> commonRatesList = rateConverters.Where(r => r.From == fromCurrency).Select(r => r.To).Intersect(rateConverters.Where(r => r.To == toCurrency).Select(r => r.From)).ToList();
            decimal tempRate = rateConverters.FirstOrDefault(r => r.From == fromCurrency && r.To == commonRatesList[0]).Rate;
            rate = rateConverters.FirstOrDefault(r => r.From == commonRatesList[0] && r.To == toCurrency).Rate;
            return ((tempRate * amount) * rate.Value);
        }
    }
}
