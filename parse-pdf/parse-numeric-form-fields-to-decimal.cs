using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (var field in doc.Form)
            {
                // Process only NumberField instances
                if (field is NumberField numberField)
                {
                    // Retrieve the raw string value from the field
                    string rawValue = numberField.Value?.ToString() ?? string.Empty;

                    // Attempt to parse the string into a decimal using invariant culture
                    if (decimal.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal numericValue))
                    {
                        Console.WriteLine($"Field '{numberField.PartialName}' = {numericValue}");

                        // Example calculation: double the value
                        decimal calculated = numericValue * 2;
                        Console.WriteLine($"  *2 = {calculated}");
                    }
                    else
                    {
                        Console.WriteLine($"Field '{numberField.PartialName}' contains non‑numeric data: '{rawValue}'");
                    }
                }
            }

            // Optional: save a copy of the PDF (no changes made)
            doc.Save("output.pdf");
        }
    }
}