using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodeChallenge.Core.Helpers
{
    public interface IConsoleUtilities
    {
        decimal ReadLine(string input);
    }

    public class ConsoleUtilities : IConsoleUtilities
    {
        public decimal ReadLine(string input)
        {
            while (true)
            {
                Console.Write(input);

                string userInput = Console.ReadLine();

                if (userInput == null) return 0;

                decimal result;

                if (decimal.TryParse(userInput, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }

                Console.Write("\nSorry, incorrect format. Enter it again, please\n");
            }
        }
    }
}
