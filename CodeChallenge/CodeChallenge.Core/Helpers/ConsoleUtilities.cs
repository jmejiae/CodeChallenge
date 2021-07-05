using CodeChallenge.Core.Currency;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CodeChallenge.Core.Helpers
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IConsoleUtilities
    {
        decimal ReadLine(string input);
        decimal ReadLine(string input, ICurrency currency);
    }

    /// <summary>
    /// Utilitie class for prevent the 
    /// </summary>
    public class ConsoleUtilities : IConsoleUtilities
    {
        private ILogger _logger;

        public ConsoleUtilities(ILogger logger)
        {
            _logger = logger;
        }

        public decimal ReadLine(string input)
        {
            while (true)
            {
                Console.Write(input);

                string userInput = Console.ReadLine();

                if (userInput == null) return 0;

                decimal result;

                if (decimal.TryParse(userInput, NumberStyles.Any, CultureInfo.InvariantCulture, out result) && result >= 0)
                {
                    return result;
                }

                _logger.Warning("Sorry, incorrect format. Enter it again, please\n");
            }
        }

        public decimal ReadLine(string input, ICurrency currency)
        {
            while (true)
            {
                Console.Write(input);

                string userInput = Console.ReadLine();

                if (userInput == null) return 0;

                decimal result;

                if (decimal.TryParse(userInput, NumberStyles.Any, CultureInfo.InvariantCulture, out result) && result >= 0)
                {
                    if (ValidateAmountPermitted(result, currency) || result == 0)
                    {
                        return result;
                    }
                    else
                    {
                        _logger.Error("Incorrectly recorded banknote or currency. ");
                    }
                }

                _logger.Warning("Sorry, enter it again, please\n");
            }
        }

        private bool ValidateAmountPermitted(decimal result, ICurrency currency)
        {
            return currency.CurrencyList.FirstOrDefault(a => a == result) != 0;
        }
    }
}
