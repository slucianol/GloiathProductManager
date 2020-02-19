using System;
using System.Collections.Generic;
using GNB.Core.Interfaces;
using GNB.Domain.Entities;
using System.Linq;
using Newtonsoft.Json;
using GNB.Core.Exceptions;

namespace GNB.Infrastructure.Services {
    public class RatesService : JsonService, IRatesService {
        private readonly IRateCacheService rateCacheService;
        private readonly ILoggerService loggerService;
        private List<RateConverter> rates;
        public RatesService(IRateServiceUri rateServiceUrl, IRateCacheService rateCacheService, ILoggerService loggerService) : base(rateServiceUrl) {
            this.rateCacheService = rateCacheService;
            this.loggerService = loggerService;
        }
        public IQueryable<RateConverter> GetRates(string currency = "") {
            try {
                string jsonString = GetHttpJsonContent();
                if (jsonString != string.Empty) {
                    rates = JsonConvert.DeserializeObject<List<RateConverter>>(jsonString).Select(r => new RateConverter {
                        From = r.From,
                        To = r.To,
                        Rate = r.Rate,
                        RateNoFloatingPoint = new GNB.Domain.ValueObject.Decimal {
                            Value = int.Parse(r.Rate.ToString().Replace(".", "")),
                            Exponent = r.Rate.ToString().Substring(r.Rate.ToString().LastIndexOf(".") + 1).Length
                        }
                    }).ToList();
                    if (rates.Count > 0) {
                        rateCacheService.PersistRatesInCache(rates);
                        if (currency != "" && currency != string.Empty) {
                            return rates.Where(r => r.From == currency).AsQueryable();
                        }
                        return rates.AsQueryable();
                    }
                }
                return rates.AsQueryable();
            } catch (ConnectionErrorException ex) {
                loggerService.Log(new Log {
                    LogType = Domain.ValueObject.LogType.Error,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
                return rateCacheService.GetCacheRates();
            } catch (CacheNotFoundException ex) {
                loggerService.Log(new Log {
                    LogType = Domain.ValueObject.LogType.Error,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
                throw;
            } catch (Exception ex) {
                loggerService.Log(new Log {
                    LogType = Domain.ValueObject.LogType.Error,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
                throw;
            }
        }
    }
}
