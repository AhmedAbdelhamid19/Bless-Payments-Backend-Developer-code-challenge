using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTax
{
    public class Sale
    {
        private List<SaleLine> saleLines;
        private decimal totalTax;
        private decimal totalValue;


        // solve Null reference exception when user press Enter, so the saleLines is null and cause error in ToString() function
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

            // here we got null it the format of the input is invalid, we got null reference exception
            // becasue we access members of saleline which is null here
            // eg. first word or last word isn't number, count of word less than 4 (<qty> <des> 'at' <unit price>)
            if (saleLine == null)
                return false;

            saleLines.Add(saleLine);
            totalTax += saleLine.Tax;
            totalValue += saleLine.LineValue;
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
            output.AppendFormat("Sales Taxes: {0:#,##0.00}", Tax);
            output.Append("\n");
            output.AppendFormat("Total: {0:#,##0.00}", TotalValue);
            return output.ToString();
        }
    }
}
