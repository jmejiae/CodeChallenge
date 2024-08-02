using CodeChallenge.Core.Exceptions;
using CodeChallenge.Core.Services;

namespace CodeChallenge.Test;

public class ChangeCalculatorTests
{
    [Fact]
    public void CalculateChange_ValidInput_ReturnsCorrectChange()
    {
        // Arrange
        var calculator = new ChangeCalculator();
        var payment = new List<decimal> { 10.00m };

        // Act
        var result = calculator.CalculateChange(5.25m, payment);

        // Assert
        Assert.Equal(2, result[2.00m]);
        Assert.Equal(1, result[0.50m]);
        Assert.Equal(1, result[0.25m]);
    }

    [Fact]
    public void CalculateChange_InsufficientPayment_ThrowsException()
    {
        // Arrange
        var calculator = new ChangeCalculator();
        var payment = new List<decimal> { 5.00m };

        // Act & Assert
        var exception = Assert.Throws<InvalidPaymentException>(() =>
            calculator.CalculateChange(5.25m, payment));

        Assert.Equal("The payment is insufficient.", exception.Message);
    }
}
