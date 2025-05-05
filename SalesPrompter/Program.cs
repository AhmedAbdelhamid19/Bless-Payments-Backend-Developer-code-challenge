using SalesTax;
using System;

namespace SalesPrompter
{
    class Program
    {
        static void Main(string[] args)
        {
            Sale sale;
            string input;

            sale = new Sale();
            Console.WriteLine("Enter sales in the format <qty> <description> at <unit price>\n" +
                "For example: 2 books at 13.25\n" +
                "Entering a blank line completes the sale\n");
            input = GetInput();
            while (!string.IsNullOrEmpty(input))
            {
                if (!sale.Add(input))
                {
                    Console.WriteLine($"\n" +
                        $"Sales should be in the format of <quantity> <description> at <unit price>\n" +
                        $"For example: 2 books at 13.25 \n" +
                        $"please focous on:\n" +
                        $"\t- type Quantity and unit price as number\n" +
                        $"\t- include Description of the product\n" +
                        $"\t- imported if it's imported item and don't forget product Name"
                    );
                }
                input = GetInput();
            }
            Console.WriteLine(sale.ToString());
            Console.WriteLine($"Sale level rounding: {sale.SaleLevelRounding}");
            Console.WriteLine("--- Press Enter to Finish ---");
            Console.ReadLine();
        }

        static string GetInput()
        {
            string result;
            Console.Write("Sale : ");
            try
            {
                result = Console.ReadLine();
            }
            catch (System.IO.IOException)
            {
                result = string.Empty;
            }
            if (!string.IsNullOrEmpty(result))
                result = result.Trim();
            return result;
        }
    }

}
