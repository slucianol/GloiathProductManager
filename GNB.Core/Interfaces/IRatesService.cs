using GNB.Domain.Entities;
using System.Linq;

namespace GNB.Core.Interfaces {
    public interface IRatesService {
        IQueryable<RateConverter> GetRates(string currency = "");
    }
}
