using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DiscountCalculator
{
    // Represents a discount entry from the XML file
    class DiscountRate
    {
        public string Code { get; set; }      // e.g., "SUMMER"
        public decimal Rate { get; set; }     // e.g., 0.10 for 10%
    }

    class Program
    {
        static void Main()
        {
            // Path to the XML file containing discount definitions
            const string discountXmlPath = "discounts.xml";

            if (!File.Exists(discountXmlPath))
            {
                Console.Error.WriteLine($"Discount file not found: {discountXmlPath}");
                return;
            }

            // Load discount rates from XML into a dictionary for fast lookup
            Dictionary<string, DiscountRate> discounts = LoadDiscounts(discountXmlPath);

            // Example list of item prices (could be loaded from elsewhere)
            List<decimal> itemPrices = new List<decimal> { 49.99m, 19.95m, 5.00m, 120.00m };

            // Compute the subtotal
            decimal subtotal = itemPrices.Sum();
            Console.WriteLine($"Subtotal: {subtotal:C}");

            // Ask user for a discount code (optional)
            Console.Write("Enter discount code (or press Enter for none): ");
            string inputCode = Console.ReadLine()?.Trim().ToUpperInvariant();

            // Determine applicable discount rate
            decimal discountRate = 0m;
            if (!string.IsNullOrEmpty(inputCode) && discounts.TryGetValue(inputCode, out DiscountRate dr))
            {
                discountRate = dr.Rate;
                Console.WriteLine($"Applying discount '{dr.Code}' at {dr.Rate:P0}");
            }
            else if (!string.IsNullOrEmpty(inputCode))
            {
                Console.WriteLine($"Discount code '{inputCode}' not recognized. No discount applied.");
            }

            // Apply discount dynamically
            decimal discountAmount = subtotal * discountRate;
            decimal total = subtotal - discountAmount;

            Console.WriteLine($"Discount amount: {discountAmount:C}");
            Console.WriteLine($"Total after discount: {total:C}");
        }

        // Loads discount definitions from an XML file into a dictionary keyed by discount code
        static Dictionary<string, DiscountRate> LoadDiscounts(string xmlPath)
        {
            // Expected XML format:
            // <Discounts>
            //   <Discount>
            //     <Code>SUMMER</Code>
            //     <Rate>0.10</Rate>
            //   </Discount>
            //   ...
            // </Discounts>

            XDocument doc = XDocument.Load(xmlPath);
            var discountElements = doc.Root?.Elements("Discount") ?? Enumerable.Empty<XElement>();

            var dict = new Dictionary<string, DiscountRate>(StringComparer.OrdinalIgnoreCase);
            foreach (var elem in discountElements)
            {
                string code = (string)elem.Element("Code");
                string rateStr = (string)elem.Element("Rate");

                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(rateStr))
                    continue; // skip malformed entries

                if (decimal.TryParse(rateStr, out decimal rate))
                {
                    // Ensure rate is between 0 and 1
                    rate = Math.Clamp(rate, 0m, 1m);
                    dict[code.Trim()] = new DiscountRate { Code = code.Trim(), Rate = rate };
                }
            }

            return dict;
        }
    }
}