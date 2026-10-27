using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Store parsed decimal values keyed by field name
        var numericValues = new Dictionary<string, decimal>();

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the PDF
            foreach (Field field in doc.Form.Fields)
            {
                // Process only text box fields (numeric input expected)
                if (field is TextBoxField textBox)
                {
                    string rawValue = textBox.Value?.Trim() ?? string.Empty;

                    // Try to parse the text as a decimal using invariant culture
                    if (decimal.TryParse(
                            rawValue,
                            NumberStyles.Number,
                            CultureInfo.InvariantCulture,
                            out decimal parsed))
                    {
                        numericValues[textBox.PartialName] = parsed;
                        Console.WriteLine($"Field '{textBox.PartialName}' parsed as {parsed}");
                    }
                    else
                    {
                        Console.WriteLine($"Field '{textBox.PartialName}' contains non-numeric value: '{rawValue}'");
                    }
                }
            }
        }

        // Example calculation: sum of all parsed numeric fields
        decimal total = 0m;
        foreach (decimal value in numericValues.Values)
        {
            total += value;
        }

        Console.WriteLine($"Total of numeric fields: {total}");
    }
}