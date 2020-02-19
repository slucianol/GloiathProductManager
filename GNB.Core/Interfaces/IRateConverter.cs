namespace GNB.Core.Interfaces {
    public interface IRateConverterService {
        decimal ConvertAmount(string fromCurrency, string toCurrency, decimal amount);
        GNB.Domain.ValueObject.Decimal ConvertAmount(string fromCurrency, string toCurrency, GNB.Domain.ValueObject.Decimal amount);
    }
}
