using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    // Represents a line item in an order
    class OrderItem
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

    static void Main()
    {
        const string discountsXmlPath = "discounts.xml";

        if (!File.Exists(discountsXmlPath))
        {
            Console.Error.WriteLine($"Discount file not found: {discountsXmlPath}");
            return;
        }

        // Load discount rates from XML into a dictionary: code => rate (e.g., 0.10 for 10%)
        Dictionary<string, decimal> discountRates = LoadDiscountRates(discountsXmlPath);

        // Example order items
        List<OrderItem> items = new List<OrderItem>
        {
            new OrderItem { Name = "Widget A", UnitPrice = 25.00m, Quantity = 2 },
            new OrderItem { Name = "Widget B", UnitPrice = 15.50m, Quantity = 3 },
            new OrderItem { Name = "Service C", UnitPrice = 100.00m, Quantity = 1 }
        };

        // Prompt user for a discount code (optional)
        Console.Write("Enter discount code (or press Enter for none): ");
        string inputCode = Console.ReadLine()?.Trim().ToUpperInvariant();

        decimal discountRate = 0m;
        if (!string.IsNullOrEmpty(inputCode) && discountRates.TryGetValue(inputCode, out decimal rate))
        {
            discountRate = rate;
            Console.WriteLine($"Applying discount code '{inputCode}' with rate {discountRate:P0}.");
        }
        else if (!string.IsNullOrEmpty(inputCode))
        {
            Console.WriteLine($"Discount code '{inputCode}' not found. No discount will be applied.");
        }

        // Calculate subtotal
        decimal subtotal = items.Sum(i => i.UnitPrice * i.Quantity);
        // Apply discount dynamically
        decimal discountAmount = subtotal * discountRate;
        decimal total = subtotal - discountAmount;

        // Output results
        Console.WriteLine("\nOrder Summary:");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Quantity} x {item.Name} @ {item.UnitPrice:C} = {(item.UnitPrice * item.Quantity):C}");
        }
        Console.WriteLine($"Subtotal: {subtotal:C}");
        Console.WriteLine($"Discount ({discountRate:P0}): -{discountAmount:C}");
        Console.WriteLine($"Total: {total:C}");
    }

    // Loads discount rates from an XML file.
    // Expected XML format:
    // <Discounts>
    //   <Discount>
    //     <Code>SUMMER</Code>
    //     <Rate>0.10</Rate>
    //   </Discount>
    //   ...
    // </Discounts>
    static Dictionary<string, decimal> LoadDiscountRates(string xmlPath)
    {
        var rates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

        // Use a using block to ensure the file stream is disposed promptly.
        using (FileStream stream = File.OpenRead(xmlPath))
        {
            XDocument doc = XDocument.Load(stream);
            foreach (XElement discountElem in doc.Root.Elements("Discount"))
            {
                string code = discountElem.Element("Code")?.Value?.Trim().ToUpperInvariant();
                string rateStr = discountElem.Element("Rate")?.Value?.Trim();

                if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(rateStr))
                    continue; // Skip malformed entries

                if (decimal.TryParse(rateStr, out decimal rate))
                {
                    // Ensure rate is between 0 and 1 (e.g., 0.15 for 15%)
                    if (rate >= 0m && rate <= 1m)
                        rates[code] = rate;
                }
            }
        }

        return rates;
    }
}