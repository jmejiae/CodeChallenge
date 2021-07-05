using CodeChallenge.Core.Helpers;
using Moq;
using NUnit.Framework;
using Serilog;
using System;
using System.IO;

namespace CodeChallenge.Test
{
    public class TestIOConsole
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetCorrectFormat_WhenIsExecuted_ReturnValue()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("5.18");
            Console.SetIn(input);

            var mockLogger = new Mock<ILogger>();

            ConsoleUtilities consoleUtilities = new ConsoleUtilities(mockLogger.Object);
            consoleUtilities.ReadLine("Please, insert a value for test:");

            Assert.That(output.ToString(), Is.EqualTo($"Please, insert a value for test:"));
        }

        [Test]
        public void GetPreventNegativeFormat_WhenIsExecuted_ReturnValue()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("-5.18");
            Console.SetIn(input);

            var mockLogger = new Mock<ILogger>();

            ConsoleUtilities consoleUtilities = new ConsoleUtilities(mockLogger.Object);
            consoleUtilities.ReadLine("Please, insert a value for test:");

            Assert.That(output.ToString(), Is.EqualTo($"Please, insert a value for test:Please, insert a value for test:"));
        }

        [Test]
        public void GetIncorrectFormat_WhenIsExecuted_ReturnValue()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("FAKE");
            Console.SetIn(input);

            var mockLogger = new Mock<ILogger>();

            ConsoleUtilities consoleUtilities = new ConsoleUtilities(mockLogger.Object);
            consoleUtilities.ReadLine("Please, insert a value for test:");

            Assert.That(output.ToString(), Is.EqualTo($"Please, insert a value for test:Please, insert a value for test:"));
        }
    }
}