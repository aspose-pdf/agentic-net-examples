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
        const string fieldName = "NumberField1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF using Aspose.Pdf Document within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the numeric form field by its name
            var numberField = doc.Form[fieldName] as NumberField;
            if (numberField == null)
            {
                Console.Error.WriteLine($"Number field '{fieldName}' not found.");
                return;
            }

            // The Value property holds the field content; convert it to string
            string rawValue = numberField.Value?.ToString() ?? string.Empty;

            // Parse the string to a decimal using invariant culture
            if (decimal.TryParse(rawValue, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal numericValue))
            {
                // Example calculation using the parsed decimal value
                decimal result = numericValue * 2m;
                Console.WriteLine($"Parsed value: {numericValue}, doubled: {result}");
            }
            else
            {
                Console.Error.WriteLine($"Unable to parse '{rawValue}' as a decimal number.");
            }
        }
    }
}