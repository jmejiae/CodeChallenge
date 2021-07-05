using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Core.Type
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface ICalculate
    {
        void CalculateChange(decimal priceItem, IEnumerable<decimal> amount);

        void CurrencyConfigured();
    }
}
