using CodeChallenge.Core.Currency;
using CodeChallenge.Core.Helpers;
using CodeChallenge.Core.Type;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeChallenge.Core
{
    public class ConsoleOutput : IConsoleOutput
    {
        private ILogger _logger;
        private IConsoleUtilities _consoleTools;
        private ICalculate _calculate;
        private ICurrency _currency;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="logger">Logger using Serilog</param>
        /// <param name="consoleTools">Utilities to read from the console app</param>
        /// <param name="calculate">Type of calculation</param>
        public ConsoleOutput(ILogger logger, IConsoleUtilities consoleTools, ICalculate calculate, ICurrency currency)
        {
            _logger = logger;
            _consoleTools = consoleTools;
            _calculate = calculate;
            _currency = currency;
        }

        /// <summary>
        /// Function for calculte the  total number of amount to be delivered
        /// </summary>
        public void CalculateItemPrice()
        {
            var coins = new List<decimal>();

            _logger.Information("App Started...");
            _calculate.CurrencyConfigured();
            var itemPrice = _consoleTools.ReadLine("Please, introduce the item price: ");
            Console.WriteLine($"\nPrice captured: ${itemPrice}");

            while (true)
            {
                var customerCoint = _consoleTools.ReadLine($"Total paid: ${ coins.Sum() }\nPlease, introduce all bills and coins to pay, 0 = finish: ", _currency);

                if (customerCoint == 0)
                {
                    break;
                }

                coins.Add(customerCoint);
            }

            _calculate.CalculateChange(itemPrice, coins);
            Console.ReadLine();
        }
    }
}
