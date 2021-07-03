using CodeChallenge.Core.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeChallenge.Core
{
    public class Calculate : ICalculate
    {
        public virtual void CalculateChange(decimal priceItem, List<decimal> amount)
        {
            decimal change = amount.Sum() - priceItem;

            if (change < decimal.Zero)
            {
                throw new Exception("The price item it's not greater that or equal to the price of the item ");
            }

            Console.WriteLine($"\nReturn change: {change.ToString("$0.00")} \t");
        }

        public virtual void CurrencyConfigured()
        {
            Console.WriteLine("Please configure the currency type");
        }
    }
}
