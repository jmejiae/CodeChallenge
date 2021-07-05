using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Core.Currency
{
    public class AmericanDolar : ICurrency
    {
        public decimal[] CurrencyList { get; set; } =
        {
            0.01m, 0.05m, 0.10m, 0.25m, 0.50m, 1.00m, 2.00m, 5.00m, 10.00m, 20.00m, 50.00m, 100.00m
        };
    }
}
