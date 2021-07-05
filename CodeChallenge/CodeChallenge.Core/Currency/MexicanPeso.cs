using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Core.Currency
{
    public class MexicanPeso : ICurrency
    {
        public decimal[] CurrencyList { get; set; } =
        {
            0.05m, 0.10m, 0.20m, 0.50m, 1m, 2m, 5m, 10m, 20m, 50m, 100m
        };
    }
}
