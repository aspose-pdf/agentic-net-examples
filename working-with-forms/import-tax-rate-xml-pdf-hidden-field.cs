using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace TaxRateUtility
{
    // Minimal replacement for ASP.NET's HiddenField when System.Web is unavailable.
    public class HiddenField
    {
        public string Value { get; set; } = string.Empty;
    }

    public static class TaxRateLoader
    {
        /// <summary>
        /// Loads tax rates from an XML file and stores them in a hidden field as JSON.
        /// The hidden field can later be read on the client side for calculations.
        /// </summary>
        /// <param name="xmlFilePath">Full path to the XML file containing tax rates.</param>
        /// <param name="hiddenField">The hidden field control to populate.</param>
        public static void LoadTaxRates(string xmlFilePath, HiddenField hiddenField)
        {
            if (string.IsNullOrWhiteSpace(xmlFilePath))
                throw new ArgumentException("XML file path must be provided.", nameof(xmlFilePath));

            if (hiddenField == null)
                throw new ArgumentNullException(nameof(hiddenField));

            if (!File.Exists(xmlFilePath))
                throw new FileNotFoundException("Tax rates XML file not found.", xmlFilePath);

            // Open the file in a using block to guarantee disposal.
            using (FileStream stream = File.OpenRead(xmlFilePath))
            {
                XDocument doc = XDocument.Load(stream);

                // Dictionary keyed by "Country|Type".
                var rates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

                // Guard against a missing root element.
                foreach (XElement rateElement in doc.Root?.Elements("Rate") ?? Array.Empty<XElement>())
                {
                    string country = (string)rateElement.Attribute("Country");
                    string type = (string)rateElement.Attribute("Type");
                    string valueStr = (string)rateElement.Attribute("Value");

                    if (string.IsNullOrWhiteSpace(country) ||
                        string.IsNullOrWhiteSpace(type) ||
                        string.IsNullOrWhiteSpace(valueStr))
                    {
                        // Skip malformed entries.
                        continue;
                    }

                    if (!decimal.TryParse(valueStr, out decimal value))
                    {
                        // Skip entries with invalid numeric values.
                        continue;
                    }

                    string key = $"{country}|{type}";
                    rates[key] = value;
                }

                // Serialize to JSON for client‑side consumption.
                string json = JsonSerializer.Serialize(rates);
                hiddenField.Value = json;
            }
        }
    }

    // Simple console entry point so the project compiles as an executable.
    internal class Program
    {
        /// <summary>
        /// Entry point required by the build system. Demonstrates loading tax rates when a file path is supplied.
        /// </summary>
        /// <param name="args">Command‑line arguments – the first argument should be the XML file path.</param>
        private static void Main(string[] args)
        {
            // If no argument is supplied we simply exit – this avoids null‑reference warnings.
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Usage: TaxRateUtility <TaxRatesXmlPath>");
                return;
            }

            string xmlPath = args[0];
            var hidden = new HiddenField();

            try
            {
                TaxRateLoader.LoadTaxRates(xmlPath, hidden);
                Console.WriteLine("Tax rates loaded successfully. JSON payload stored in hidden field.");
                Console.WriteLine(hidden.Value);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
