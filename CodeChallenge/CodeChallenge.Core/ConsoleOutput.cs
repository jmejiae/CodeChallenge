using CodeChallenge.Core.Helpers;
using CodeChallenge.Core.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeChallenge.Core
{
    public class ConsoleOutput : IConsoleOutput
    {
        private ConsoleUtilities _consoleTools;
        private ICalculate _calculation;

        public ConsoleOutput(ConsoleUtilities consoleTools, ICalculate calculation)
        {
            _consoleTools = consoleTools;
            _calculation = calculation;
        }

        public void CalculateItemPrice()
        {
            var coins = new List<decimal>();

            _calculation.CurrencyConfigured();
            var itemPrice = _consoleTools.ReadLine("Please, introduce the item price: ");
            Console.WriteLine($"The price it's: ${itemPrice}\n");

            while (true)
            {
                var customerCoint = _consoleTools.ReadLine($"Amount: ${ coins.Sum() }\nPlease, introduce all bills and coins to pay, 0 = finish: ");

                if (customerCoint == 0)
                {
                    break;
                }

                coins.Add(customerCoint);
            }

            _calculation.CalculateChange(itemPrice, coins);
            Console.ReadLine();
        }
    }
}
