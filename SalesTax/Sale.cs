using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SalesTax
{
    public class Sale
    {
        private List<SaleLine> saleLines;
        private decimal totalTax;
        private decimal totalValue;
        public double totalBeforeRounding = 0;
        public double TotalTaxBeforeRounding = 0;
        public double SaleLevelRounding = 0;

        ///<summary>
        /// ctor to solve Null reference exception when user press Enter,
        /// without it and when user press Enter without type anything
        /// so the saleLines will be null and cause error in ToString() function
        ///</summary>
        public Sale()
        {
            saleLines = new List<SaleLine>();
            totalTax = 0;
            totalValue = 0;
        }

        /// <summary>
        /// Adds a line to the sale.
        /// </summary>
        /// <param name="inputLine">The line to add.</param>
        /// <returns>True for success, False for failure.  Failures are usually caused via incorrect formatting of the input</returns>
        public bool Add(string inputLine)
        {
            SaleLine saleLine;

            saleLine = InputParser.ProcessInput(inputLine);

            /// <summary>
            /// here we got null reference exception if the format of the input is invalid and can't be processed,
            /// becasue we access members of saleline which is null here
            /// eg. first word or last word isn't number, count of word less than 4 (<qty> <des> 'at' <unit price>) ...etc. 
            /// </summary>
            if (saleLine == null)
                return false;

            saleLines.Add(saleLine);
            totalTax += saleLine.Tax;
            totalValue += saleLine.LineValue;
            totalBeforeRounding += saleLine.BeforeRounding;
            TotalTaxBeforeRounding += saleLine.TaxBeforeRounding;
            SaleLevelRounding = totalBeforeRounding + TotalTaxBeforeRounding + (TotalTaxBeforeRounding % 0.05 > 0 ? 0.05 - TotalTaxBeforeRounding % 0.05 : 0);
            return true;
        }


        /// <summary>
        /// The total Tax amount for the sale
        /// </summary>
        public decimal Tax
        {
            get { return totalTax; }
        }

        /// <summary>
        /// The total value of the sale (including Tax)
        /// </summary>
        public decimal TotalValue
        {
            get { return totalValue; }
        }


        /// <summary>
        /// Converts the sale to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            foreach (SaleLine line in saleLines)
            {
                if (output.Length > 0)
                    output.Append("\n");
                output.Append(line.ToString());
            }
            //Now add footer information
            output.Append("\n");
            output.AppendLine($"Sales Taxes: {Tax.ToString("N2", CultureInfo.CurrentCulture)}");
            output.Append("\n");
            output.Append($"Total: {TotalValue.ToString("N2", CultureInfo.CurrentCulture)}");
            return output.ToString();
        }
    }
}
