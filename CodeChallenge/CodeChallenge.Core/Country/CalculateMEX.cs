using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeChallenge.Core.Type
{
    public class CalculateMEX : Calculate, ICalculate
    {
        public override void CalculateChange(decimal priceItem, List<decimal> amount)
        {
            base.CalculateChange(priceItem, amount);

            decimal change = amount.Sum() - priceItem;

            while (change >= 100)
            {
                Console.Write("$100 ");
                change -= 100;
            }
            while (change >= 50)
            {
                Console.Write("$50 ");
                change -= 50;
            }
            while (change >= 20)
            {
                Console.Write("$20 ");
                change -= 20;
            }
            while (change >= 10)
            {
                Console.Write("$10 ");
                change -= 10;
            }
            while (change >= 5)
            {
                Console.Write("$5 ");
                change -= 5;
            }
            while (change >= 2)
            {
                Console.Write("$2 ");
                change -= 2;
            }
            while (change >= 1)
            {
                Console.Write("$1 ");
                change -= 1;
            }
            while (change >= 0.5m)
            {
                Console.Write("$0.50 ");
                change -= 0.5m;
            }
            while (change >= 0.20m)
            {
                Console.Write("$0.20 ");
                change -= 0.20m;
            }
            while (change >= 0.10m)
            {
                Console.Write("$0.10 ");
                change -= 0.10m;
            }
            while (change >= 0.05m)
            {
                Console.Write("$0.05 ");
                change -= 0.05m;
            }

            Console.WriteLine();
        }

        public override void CurrencyConfigured()
        {
            Console.WriteLine("MEX bill configured");
        }
    }
}
