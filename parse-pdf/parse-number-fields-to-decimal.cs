using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document containing form fields
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all form fields in the document
            foreach (var field in doc.Form)
            {
                // Process only NumberField instances
                if (field is NumberField numberField)
                {
                    // Retrieve the raw value (may be null)
                    string rawValue = numberField.Value?.ToString() ?? string.Empty;

                    // Attempt to parse the value to a decimal using invariant culture
                    if (decimal.TryParse(rawValue, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal numericValue))
                    {
                        Console.WriteLine($"Field '{numberField.PartialName}' parsed value: {numericValue}");

                        // Example calculation: double the numeric value
                        decimal result = numericValue * 2;
                        Console.WriteLine($"Calculated result (value * 2): {result}");
                    }
                    else
                    {
                        Console.WriteLine($"Field '{numberField.PartialName}' contains non-numeric data: '{rawValue}'");
                    }
                }
            }
        }
    }
}