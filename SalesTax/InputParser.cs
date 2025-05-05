using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTax
{
    // THIS IS NOT THREAD SAFE (or localised)
    public static class InputParser
    {

        // i prefered to seperate logic of these 2 functions from inside ProcessInput Function to achive 
        // single responsibility principle, open for extension and closed for modification
        // (and alot of other principles) and make the code more readable, Maintainable, Reusable and Testable
        // which is better than provided version which is like Monolithic Code

        ///<summary>
        /// Check and normalize to lowercase, replace ['Imported' ..etc] with 'imported' word
        /// if a character typed in uppercase, by mistake by the user, and same for book, tablet, chip
        /// , the same for book, tablet and chip
        ///</summary>
        
        public static List<string> SpecialItems = new List<string>
        {
            "book", "books", "tablet", "tablets", "chip", "chips", "chocolate", "chocolates"
        };
        private static void ProcessWords(string[] words, int wordCount, ref bool isImported)
        {
            for (int i = 1; i < wordCount - 2; i++)
            {

                if (words[i].Equals("imported", StringComparison.OrdinalIgnoreCase))
                {
                    words[i] = "imported";
                    isImported = true;
                }

                foreach (var word in SpecialItems)
                {
                    if (words[i].Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        words[i] = word;
                    }
                }
            }
        }
        private static bool IsValidInput(string[] words, int wordCount, ref int quantity, ref decimal price)
        {
            ///<summary>
            /// must have at least 4 words <- was handled in a wrong way
            /// quantity, 'at', price and item name
            /// 'if (wordCount > 4)' should replace by 'if (wordCount < 4)'
            ///</summary>
            if (wordCount < 4)
                return false;

            ///<summary>
            /// if the second last word is not 'at' then it's invalid input
            /// because the format is <qty> <product> at <price> and by logic
            /// can't detect the price (may that number related to item name)
            ///</summary>
            if (!string.Equals(words[wordCount - 2], "at", StringComparison.OrdinalIgnoreCase))
                return false;

            ///<summary>
            /// get and check the first word (quantity) and last word (price)
            ///</summary>
            if (!int.TryParse(words[0], out quantity) || !decimal.TryParse(words[wordCount - 1], out price))
                return false;

            return true;
        }

        // Assumes that all input is in the format:
        //  <qty> <product> at <price>
        //
        //  If <product> contains the word imported then the product is deemed to attract import tax
        //
        // If it can't be parsed we return null.
        // If it can then we return a sales line, complete with tax information calculated.
        public static SaleLine ProcessInput(string input)
        {
            int quantity = 0;
            string productName;
            decimal price = 0;
            bool isImported = false;
            SaleLine saleLine;

            if (string.IsNullOrEmpty(input))
                return null;
            string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int wordCount = words.Length;

            if(!IsValidInput(words, wordCount, ref quantity, ref price))
                return null;
            ProcessWords(words, wordCount, ref isImported);

            ///<summary>
            /// Avoid out of range exception by excluding [quantity, 'at', price] words (wordCount - 3) instead (wordCount)
            ///</summary>
            productName = string.Join(" ", words, 1, wordCount - 3);
            if (string.IsNullOrEmpty(productName))
                return null;

            if (isImported)
            {
                // in this case productName will be 'imported' which isn't product name
                if (wordCount == 4)
                    return null;
                //Ensure the word imported appears at the front of the description
                productName = "imported " + productName.Replace("imported ", string.Empty);
            }

            // create the sale line
            saleLine = new SaleLine(quantity, productName, price, isImported);
            return saleLine;
        }

    }
}
