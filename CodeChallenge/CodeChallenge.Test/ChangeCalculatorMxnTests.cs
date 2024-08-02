using CodeChallenge.Core;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CodeChallenge.Test
{
    public class ChangeCalculatorMxnTests
    {
        private readonly ChangeCalculator _changeCalculator;

        public ChangeCalculatorMxnTests()
        {
            var currencyConfig = new MxnCurrencyConfiguration();
            _changeCalculator = new ChangeCalculator(currencyConfig);
        }

        [Fact]
        public void CalculateChange_SufficientChange_ReturnsCorrectDenominations()
        {
            // Arrange
            decimal price = 5.75m;
            decimal amountProvided = 10.00m;

            // Act
            var change = _changeCalculator.CalculateChange(price, amountProvided);

            // Assert
            var expectedChange = new Dictionary<decimal, int>
            {
                { 2.00m, 1 },
                { 2.00m, 1 },
                { 0.25m, 1 }
            };
            Assert.Equal(expectedChange, change);
        }

        [Fact]
        public void CalculateChange_InsufficientAmount_ThrowsException()
        {
            // Arrange
            decimal price = 5.75m;
            decimal amountProvided = 5.00m;

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                _changeCalculator.CalculateChange(price, amountProvided));
            Assert.Equal("Amount provided is less than the price.", exception.Message);
        }
    }
}
