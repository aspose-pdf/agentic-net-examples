using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "invoice.pdf";
        const string outputXmlPath = "total_price.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Extract all text from the document
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                string allText = absorber.Text;

                // Find all monetary values (e.g., $123.45 or 123.45) and sum them
                decimal totalPrice = 0m;
                // Regex matches optional currency symbol followed by digits, optional decimal part
                Regex priceRegex = new Regex(@"\$?\s*(\d{1,3}(?:,\d{3})*(?:\.\d{2})?)", RegexOptions.Compiled);
                foreach (Match match in priceRegex.Matches(allText))
                {
                    // Remove commas and possible currency symbol, then parse
                    string numericPart = match.Groups[1].Value.Replace(",", "");
                    if (decimal.TryParse(numericPart, out decimal price))
                    {
                        totalPrice += price;
                    }
                }

                // Store the calculated total price as custom metadata using DocumentInfo indexer
                doc.Info["TotalPrice"] = totalPrice.ToString("F2");

                // Save the document as XML using the required XmlSaveOptions
                XmlSaveOptions xmlOptions = new XmlSaveOptions();
                doc.Save(outputXmlPath, xmlOptions);
            }

            Console.WriteLine($"Total price exported to XML: {outputXmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}