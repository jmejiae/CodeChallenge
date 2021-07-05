using CodeChallenge.Core.Currency;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeChallenge.Core.Type
{
    public class CalculateUSA : Calculate, ICalculate
    {
        private readonly ILogger _logger;
        private readonly ICurrency _currency;

        public CalculateUSA(ILogger logger, ICurrency currency) : base(logger)
        {
            _logger = logger;
            _currency = currency;
        }

        public override void CurrencyConfigured()
        {
            _logger.Information("USA bill configured\n");
        }

        public override void CalculateChange(decimal priceItem, List<decimal> amount)
        {
            base.CalculateChange(priceItem, amount);
            
            decimal change = amount.Sum() - priceItem;

            if (change == decimal.Zero)
            {
                Console.Write($"$0 ");
            }

            foreach (var currency in _currency.CurrencyList.OrderByDescending(d => d))
            {
                while(change >= currency)
                {
                    Console.Write($"${currency} ");
                    change -= currency;
                }
            }

            Console.WriteLine(Environment.NewLine);
            _logger.Information("App finished...");
        }
    }
}
