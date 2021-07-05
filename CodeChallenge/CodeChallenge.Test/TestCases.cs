using CodeChallenge.Core;
using CodeChallenge.Core.Currency;
using CodeChallenge.Core.Helpers;
using CodeChallenge.Core.Type;
using Moq;
using NUnit.Framework;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeChallenge.Test
{
    public class TestCases
    {
        private Mock<ILogger> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger>();
        }

        [Test]
        public void ItemPriceZero_USA_WhenIsExecuted_ReturnValue()
        {
            string priceItem = "0"; // Item price
            string totalCoins = "" + "0"; // list of coins that will be validate
            decimal totalAmountChange = 0m; // total amount of the item price in decimal
            decimal expectedItem = 0m; // expected value of the 
            string expectedCoins = "$0.00"; // expected coins in string ($0.05 $0.10 $0.20)

            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader($"{priceItem}\n{totalCoins}");
            Console.SetIn(input);

            var mockLogger = new Mock<ILogger>();
            var mockConsoleUtilities = new Mock<ConsoleUtilities>(mockLogger.Object);
            var mockAmericanDollar = new Mock<AmericanDolar>();
            var mockCalculateUSA = new Mock<CalculateUSA>(mockLogger.Object, mockAmericanDollar.Object);

            // Setup
            var consoleUtilities = new ConsoleUtilities(_mockLogger.Object);
            var americanDollar = new AmericanDolar();
            var calculateUSA = new CalculateUSA(_mockLogger.Object, americanDollar);
            
            var consoleOutput = new ConsoleOutput(_mockLogger.Object, consoleUtilities, calculateUSA, americanDollar);
            var expectedOutputResult = $"Please, introduce the item price: \nPrice captured: ${ expectedItem.ToString("0.00") }\r\nTotal paid: $0.00\nPlease, introduce all bills and coins to pay, 0 = finish: \nReturn change: { totalAmountChange.ToString("$0.00") } or => { expectedCoins } ";

            // Act
            consoleOutput.CalculateItemPrice();

            // Assert
            Assert.That(output.ToString, Is.EqualTo(expectedOutputResult));
        }

        [Test]
        public void ItemPriceFive_USA_WhenIsExecuted_ReturnValue()
        {
            string itemPrice = "5"; // price of the item
            string toPay = "5\n0"; // quantity of money/notes to pay and validate, at the end add a zero in order to indicate finish
            decimal expectedChange = 0m; // expected total amount
            string expectedChangeOfStrings = "$0.00"; // Final result expected of all the money/notes, i.e.: ($0.10 $0.05...)
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader($"{itemPrice}\n{toPay}");
            Console.SetIn(input);

            var mockLogger = new Mock<ILogger>();
            var mockConsoleUtilities = new Mock<ConsoleUtilities>(mockLogger.Object);
            var mockAmericanDollar = new Mock<AmericanDolar>();
            var mockCalculateUSA = new Mock<CalculateUSA>(mockLogger.Object, mockAmericanDollar.Object);

            // Setup
            var consoleUtilities = new ConsoleUtilities(_mockLogger.Object);
            var americanDollar = new AmericanDolar();
            var calculateUSA = new CalculateUSA(_mockLogger.Object, americanDollar);

            var consoleOutput = new ConsoleOutput(_mockLogger.Object, consoleUtilities, calculateUSA, americanDollar);
            var expectedOutputResult = $"Please, introduce the item price: \n" +
                $"Price captured: ${ decimal.Parse(itemPrice).ToString("0.00") }\r\n" +
                $"Total paid: $0.00\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: Total paid: $5.00\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: \n" +
                $"Return change: ${ expectedChange.ToString("0.00") } or => { expectedChangeOfStrings } ";

            // Act
            consoleOutput.CalculateItemPrice();

            // Assert
            Assert.That(output.ToString, Is.EqualTo(expectedOutputResult));
        }

        [Test]
        public void ItemPriceFiveEighteen_USA_WhenIsExecuted_ReturnValue()
        {
            string itemPrice = "5.18"; // price of the item
            string toPay = "5\n0.1\n0.1\n0"; // quantity of money/notes to pay and validate, at the end add a zero in order to indicate finish
            decimal expectedChange = 0.02m; // expected total amount
            string expectedChangeOfStrings = "$0.01 $0.01"; // Final result expected of all the money/notes, i.e.: ($0.10 $0.05...)
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader($"{itemPrice}\n{toPay}");
            Console.SetIn(input);

            var mockLogger = new Mock<ILogger>();
            var mockConsoleUtilities = new Mock<ConsoleUtilities>(mockLogger.Object);
            var mockAmericanDollar = new Mock<AmericanDolar>();
            var mockCalculateUSA = new Mock<CalculateUSA>(mockLogger.Object, mockAmericanDollar.Object);

            // Setup
            var consoleUtilities = new ConsoleUtilities(_mockLogger.Object);
            var americanDollar = new AmericanDolar();
            var calculateUSA = new CalculateUSA(_mockLogger.Object, americanDollar);

            var consoleOutput = new ConsoleOutput(_mockLogger.Object, consoleUtilities, calculateUSA, americanDollar);
            var expectedOutputResult = $"Please, introduce the item price: \n" +
                $"Price captured: ${ decimal.Parse(itemPrice).ToString("0.00") }\r\n" +
                $"Total paid: $0.00\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: " +
                $"Total paid: $5.00\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: " +
                $"Total paid: $5.10\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: Total paid: $5.20\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: \n" +
                $"Return change: ${ expectedChange.ToString("0.00") } or => { expectedChangeOfStrings } \r\n\r\n";

            // Act
            consoleOutput.CalculateItemPrice();

            // Assert
            Assert.That(output.ToString, Is.EqualTo(expectedOutputResult));
        }

        [Test]
        public void ItemPriceZero_MEX_WhenIsExecuted_ReturnValue()
        {
            string priceItem = "0"; // Item price
            string totalCoins = "" + "0"; // list of coins that will be validate
            decimal totalAmountChange = 0m; // total amount of the item price in decimal
            decimal expectedItem = 0m; // expected value of the 
            string expectedCoins = "$0"; // expected coins in string ($0.05 $0.10 $0.20)

            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader($"{priceItem}\n{totalCoins}");
            Console.SetIn(input);

            var mockLogger = new Mock<ILogger>();
            var mockConsoleUtilities = new Mock<ConsoleUtilities>(mockLogger.Object);
            var mockAmericanDollar = new Mock<MexicanPeso>();
            var mockCalculateUSA = new Mock<CalculateMEX>(mockLogger.Object, mockAmericanDollar.Object);

            // Setup
            var consoleUtilities = new ConsoleUtilities(_mockLogger.Object);
            var americanDollar = new MexicanPeso();
            var calculateUSA = new CalculateMEX(_mockLogger.Object, americanDollar);

            var consoleOutput = new ConsoleOutput(_mockLogger.Object, consoleUtilities, calculateUSA, americanDollar);
            var expectedOutputResult = $"Please, introduce the item price: \n" +
                $"Price captured: ${ expectedItem.ToString("0.00") }\r\n" +
                $"Total paid: $0.00\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: \n" +
                $"Return change: { totalAmountChange.ToString("$0.00") } or => { expectedCoins } \r\n\r\n";

            // Act
            consoleOutput.CalculateItemPrice();

            // Assert
            Assert.That(output.ToString, Is.EqualTo(expectedOutputResult));
        }

        [Test]
        public void ItemPriceFive_MEX_WhenIsExecuted_ReturnValue()
        {
            string itemPrice = "5"; // price of the item
            string toPay = "5\n0"; // quantity of money/notes to pay and validate, at the end add a zero in order to indicate finish
            decimal expectedChange = 0m; // expected total amount
            string expectedChangeOfStrings = "$0"; // Final result expected of all the money/notes, i.e.: ($0.10 $0.05...)
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader($"{itemPrice}\n{toPay}");
            Console.SetIn(input);

            var mockLogger = new Mock<ILogger>();
            var mockConsoleUtilities = new Mock<ConsoleUtilities>(mockLogger.Object);
            var mockAmericanDollar = new Mock<MexicanPeso>();
            var mockCalculateUSA = new Mock<CalculateMEX>(mockLogger.Object, mockAmericanDollar.Object);

            // Setup
            var consoleUtilities = new ConsoleUtilities(_mockLogger.Object);
            var americanDollar = new MexicanPeso();
            var calculateUSA = new CalculateMEX(_mockLogger.Object, americanDollar);

            var consoleOutput = new ConsoleOutput(_mockLogger.Object, consoleUtilities, calculateUSA, americanDollar);
            var expectedOutputResult = $"Please, introduce the item price: \n" +
                $"Price captured: ${ decimal.Parse(itemPrice).ToString("0.00") }\r\n" +
                $"Total paid: $0.00\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: Total paid: $5.00\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: \n" +
                $"Return change: ${ expectedChange.ToString("0.00") } or => { expectedChangeOfStrings } \r\n\r\n";

            // Act
            consoleOutput.CalculateItemPrice();

            // Assert
            Assert.That(output.ToString, Is.EqualTo(expectedOutputResult));
        }

        [Test]
        public void ItemPriceFiveEighteen_MEX_WhenIsExecuted_ReturnValue()
        {
            string itemPrice = "5.15"; // price of the item
            string toPay = "5\n0.1\n0.1\n0"; // quantity of money/notes to pay and validate, at the end add a zero in order to indicate finish
            decimal expectedChange = 0.05m; // expected total amount
            string expectedChangeOfStrings = "$0.05"; // Final result expected of all the money/notes, i.e.: ($0.10 $0.05...)
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader($"{itemPrice}\n{toPay}");
            Console.SetIn(input);

            var mockLogger = new Mock<ILogger>();
            var mockConsoleUtilities = new Mock<ConsoleUtilities>(mockLogger.Object);
            var mockAmericanDollar = new Mock<MexicanPeso>();
            var mockCalculateUSA = new Mock<CalculateMEX>(mockLogger.Object, mockAmericanDollar.Object);

            // Setup
            var consoleUtilities = new ConsoleUtilities(_mockLogger.Object);
            var americanDollar = new MexicanPeso();
            var calculateUSA = new CalculateMEX(_mockLogger.Object, americanDollar);

            var consoleOutput = new ConsoleOutput(_mockLogger.Object, consoleUtilities, calculateUSA, americanDollar);
            var expectedOutputResult = $"Please, introduce the item price: \n" +
                $"Price captured: ${ decimal.Parse(itemPrice).ToString("0.00") }\r\n" +
                $"Total paid: $0.00\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: " +
                $"Total paid: $5.00\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: " +
                $"Total paid: $5.10\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: Total paid: $5.20\n" +
                $"Please, introduce all bills and coins to pay, 0 = finish: \n" +
                $"Return change: ${ expectedChange.ToString("0.00") } or => { expectedChangeOfStrings } \r\n\r\n";

            // Act
            consoleOutput.CalculateItemPrice();

            // Assert
            Assert.That(output.ToString, Is.EqualTo(expectedOutputResult));
        }
    }
}
