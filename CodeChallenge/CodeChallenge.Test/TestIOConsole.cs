using CodeChallenge.Core.Helpers;
using NUnit.Framework;
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

            ConsoleUtilities consoleUtilities = new ConsoleUtilities();
            consoleUtilities.ReadLine("Please, insert a value for test:");

            Assert.That(output.ToString(), Is.EqualTo($"Please, insert a value for test:"));
        }

        [Test]
        public void GetIncorrectFormat_WhenIsExecuted_ReturnValue()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("FAKE");
            Console.SetIn(input);

            ConsoleUtilities consoleUtilities = new ConsoleUtilities();
            consoleUtilities.ReadLine("Please, insert a value for test:");

            Assert.That(output.ToString(), Is.EqualTo($"Please, insert a value for test:\nSorry, incorrect format. Enter it again, please\nPlease, insert a value for test:"));
        }
    }
}