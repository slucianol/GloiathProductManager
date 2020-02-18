namespace GNB.Core.Interfaces {
    public interface IRateConverterService {
        decimal ConvertAmount(string fromCurrency, string toCurrency, decimal amount);
    }
}
