using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTax
{
    // THIS IS NOT THREAD SAFE (or localised)
    public static class InputParser
    {

        // Assumes that all input is in the format:
        //  <qty> <product> at <price>
        //
        //  If <product> contains the word imported then the product is deemed to attract import tax
        //
        // If it can't be parsed we return null.
        // If it can then we return a sales line, complete with tax information calculated.
        public static SaleLine ProcessInput(string input)
        {
            int quantity;
            string productName;
            decimal price;
            bool isImported;
            SaleLine saleLine;

            if (string.IsNullOrEmpty(input))
                return null;
            string[] words = input.Split(' ');
            int wordCount = words.Length;

            // must have at least 4 words <- was handled in a wrong way
            // this line handle the case that the product should contain at lease 4 words 
            // one to quantity, ont to description, one to 'at' word, and one to price
            // 'if (wordCount > 4)' should replace by 'if (wordCount < 4)'
            if (wordCount < 4)
                return null; 

            // get quantity (first word)
            try
            {
                quantity = int.Parse(words[0]);
            }
            catch (FormatException)
            {
                return null;
            }
            catch (OverflowException)
            {
                return null;
            }


            // get price (last word in input string)
            try
            {
                price = decimal.Parse(words[wordCount - 1]);
            }
            catch (FormatException)
            {
                return null;
            }
            catch (OverflowException)
            {
                return null;
            }


            // solving out of range exception, by replace the fourth parameter with wordCount - 2
            // which lead to out of range exception, we should escape first and last word only
            productName = string.Join(" ", words, 1, wordCount - 2);
            if (string.IsNullOrEmpty(productName))
                return null;

            //Check if this is an imported product
            isImported = productName.Contains("imported ");
            if (isImported)
            {
                //Ensure the word imported appears at the front of the description
                productName = "imported " + productName.Replace("imported ", string.Empty);
            }

            // create the sale line
            saleLine = new SaleLine(quantity, productName, price, isImported);
            return saleLine;
        }

    }
}
