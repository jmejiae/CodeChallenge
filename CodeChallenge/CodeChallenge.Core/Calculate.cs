using CodeChallenge.Core.Type;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeChallenge.Core
{
    public class Calculate : ICalculate
    {
        private ILogger _logger;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="logger">Logger using Serilog</param>
        public Calculate(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Function for only indicate in the console, the current type
        /// </summary>
        public virtual void CurrencyConfigured()
        {
            Console.WriteLine("Please configure the currency type");
        }

        /// <summary>
        /// Calculate betwenn the price of the item and the list of amount
        /// </summary>
        /// <param name="priceItem">Price Item</param>
        /// <param name="amount">List of ammounts</param>
        public virtual void CalculateChange(decimal priceItem, IEnumerable<decimal> amount)
        {
            decimal change = amount.Sum() - priceItem;

            if (change < decimal.Zero)
            {
                _logger.Error("The price item it's not greater that or equal to the price of the item ");
            }

            Console.Write($"\nReturn change: { change.ToString("$0.00") } or => ");
        }
    }
}
