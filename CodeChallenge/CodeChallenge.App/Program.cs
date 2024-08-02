using CodeChallenge.Core.Exceptions;
using CodeChallenge.Core.Services;
using Microsoft.Extensions.Configuration;


var configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .Build();

string currency = configuration["DefaultCurrency"];
Console.WriteLine($"Currency configuration: {currency}");

// Set the currency denominations based on configuration
ChangeCalculator.SetCurrencyDenominations(currency);

// Request price with validation
decimal price;
Console.Write("Enter the price of the item: ");
while (!decimal.TryParse(Console.ReadLine(), out price))
{
    Console.WriteLine("Invalid input. Please enter a valid price (numeric value).");
}

// Request payment with validation
Console.Write("Enter the denominations given by the customer (comma separated): ");
var paymentInput = Console.ReadLine().Split(',');
List<decimal> payment = new List<decimal>();

foreach (var input in paymentInput)
{
    if (decimal.TryParse(input.Trim(), out decimal denomination))
    {
        payment.Add(denomination);
    }
    else
    {
        Console.WriteLine($"Invalid denomination '{input}'. It will be treated as 0.");
        payment.Add(0); // Add 0 for invalid denominations
    }
}

var changeCalculator = new ChangeCalculator();

try
{
    var change = changeCalculator.CalculateChange(price, payment);
    Console.WriteLine("Change to be returned:");

    foreach (var item in change)
    {
        Console.WriteLine($"{item.Value} of {item.Key:C}");
    }
}
catch (InvalidPaymentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
}