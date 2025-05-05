using System;
using System.Globalization;

namespace SalesTax
{
    public class SaleLine
    {

        #region Private Member Variables
        private string productName;
        private decimal price;
        private bool isImported;
        private int quantity;
        private decimal taxAmount;
        private decimal lineValue;
        #endregion

        #region Public Properties
        public string ProductName
        {
            get { return productName; }
        }

        public decimal Price
        {
            get { return price; }
        }

        public bool IsImported
        {
            get { return isImported; }
        }

        public int Quantity
        {
            get { return quantity; }
        }

        public decimal LineValue
        {
            get { return lineValue; }
        }

        public decimal Tax
        {
            get { return taxAmount; }
        }
        #endregion

        public double BeforeRounding = 0;
        public double TaxBeforeRounding = 0;

        /// <summary>
        /// Default constructor is not publicly accesible
        /// </summary>
        private SaleLine()
        {
        }

        public int CalculateTaxRate(string productName, bool isImported)
        {
            int taxRate = 0;

            if (isImported)
                taxRate = 5;
            else
                taxRate = 0;

            bool flag = false;
            foreach (var word in InputParser.SpecialItems)
            {
                if (productName.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                taxRate += 10;
            return taxRate;
        }

        /// <summary>
        /// Public constructor for the sale line
        /// </summary>
        /// <param name="lineQuantity">Quantity on order</param>
        /// <param name="name">name of the product</param>
        /// <param name="unitPrice">price of a single item</param>
        /// <param name="itemIsImported">flag to indicate if the item is imported</param>
        public SaleLine(int lineQuantity, string name, decimal unitPrice, bool itemIsImported)
        {
            int taxRate;
            if (string.IsNullOrEmpty(name)) name = string.Empty;

            quantity = lineQuantity;
            productName = name;
            price = unitPrice;
            isImported = itemIsImported;
            lineValue = price * quantity;

            // calculate taxable amount
            // ideally should really have a product list and tax rules, but this'll have to do for the exercise.

            taxRate = CalculateTaxRate(productName, isImported);
            taxAmount = CalculateTax(lineValue, taxRate);
            BeforeRounding = (double) LineValue;
            TaxBeforeRounding = (double) LineValue * taxRate / 100;
            //Add tax to line value
            lineValue += taxAmount;
            return;
        }

        /// <summary>
        /// Calculates the amount of tax for a value, rounded up to the nearest 5 cents
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="taxRate">The tax rate to apply</param>
        /// <returns>The calculated tax on the original value</returns>
        public static decimal CalculateTax(decimal value, int taxRate)
        {
            double amount;
            double remainder;

            // here i removed rounding before get nearest 5 cents, which can cause wrong result
            amount = (double) (value * taxRate) / 100 ;

            //Now round up to nearest 5 cents.
            remainder = amount % 0.05;
            if (remainder > 0)
                amount += 0.05 - remainder;

            return (decimal)amount;
        }

        /// <summary>
        /// Converts the sale line to a string
        /// </summary>
        /// <returns>The string representation of the sale line</returns>
        public override string ToString()
        {
            return $"{Quantity} {ProductName}: {LineValue.ToString("N2", CultureInfo.CurrentCulture)}";
        }
    }
}
