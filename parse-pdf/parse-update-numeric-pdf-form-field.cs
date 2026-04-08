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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Name of the numeric field to process
            const string fieldName = "NumberField1";

            // Retrieve the field as a NumberField instance
            NumberField numberField = doc.Form[fieldName] as NumberField;
            if (numberField == null)
            {
                Console.Error.WriteLine($"Number field '{fieldName}' not found.");
                return;
            }

            // Get the field's current value as a string
            string textValue = numberField.Value?.ToString() ?? string.Empty;

            // Parse the string into a decimal using invariant culture
            if (decimal.TryParse(textValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal numericValue))
            {
                // Example calculation: add a constant value
                decimal result = numericValue + 10.5m;
                Console.WriteLine($"Original value: {numericValue}, After calculation: {result}");

                // Optionally write the result back to the field
                numberField.Value = result.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                Console.Error.WriteLine($"Unable to parse '{textValue}' as a decimal number.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}