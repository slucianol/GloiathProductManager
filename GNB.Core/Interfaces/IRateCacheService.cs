using GNB.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GNB.Core.Interfaces {
    public interface IRateCacheService {
        IQueryable<RateConverter> GetCacheRates();
        void PersistRatesInCache(List<RateConverter> rates);
        bool ExistsRatesCache();
    }
}
