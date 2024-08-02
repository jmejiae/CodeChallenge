namespace CodeChallenge.Core.Models;

public class Currency
{
    public decimal Value { get; set; }

    public Currency(decimal value)
    {
        Value = value;
    }
}
