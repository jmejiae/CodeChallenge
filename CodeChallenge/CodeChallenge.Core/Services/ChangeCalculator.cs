using CodeChallenge.Core.Exceptions;

namespace CodeChallenge.Core.Services;

public class ChangeCalculator
{
    private static List<decimal> _denominations;

    // Initialize default denominations
    static ChangeCalculator()
    {
        SetCurrencyDenominations("USD"); // Set default to USD initially
    }

    public static void SetCurrencyDenominations(string currency)
    {
        if (currency.Equals("MXN", StringComparison.OrdinalIgnoreCase))
        {
            _denominations = new List<decimal> { 0.05m, 0.10m, 0.20m, 0.50m, 1.00m, 2.00m, 5.00m, 10.00m, 20.00m, 50.00m, 100.00m };
        }
        else // Default to USD
        {
            _denominations = new List<decimal> { 0.01m, 0.05m, 0.10m, 0.25m, 0.50m, 1.00m, 2.00m, 5.00m, 10.00m, 20.00m, 50.00m, 100.00m };
        }

        _denominations = _denominations.OrderByDescending(d => d).ToList();
    }

    public Dictionary<decimal, int> CalculateChange(decimal price, List<decimal> payment)
    {
        decimal totalPayment = payment.Sum();
        if (totalPayment < price)
        {
            throw new InvalidPaymentException("The payment is insufficient.");
        }

        decimal change = totalPayment - price;
        var changeDistribution = new Dictionary<decimal, int>();

        foreach (var denomination in _denominations)
        {
            while (change >= denomination)
            {
                if (!changeDistribution.ContainsKey(denomination))
                {
                    changeDistribution[denomination] = 0;
                }
                changeDistribution[denomination]++;
                change -= denomination;
                change = Math.Round(change, 2); // To avoid precision errors
            }
        }

        return changeDistribution;
    }
}
