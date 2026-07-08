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
        const string outputPath = "parsed_numbers.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Prepare an output file to store parsed values
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Iterate over all form fields in the document
                foreach (Field field in doc.Form.Fields)
                {
                    // Process only NumberField instances
                    if (field is NumberField numberField)
                    {
                        // Retrieve the raw value as string
                        string rawValue = numberField.Value?.ToString() ?? string.Empty;

                        // Try to parse the string into a decimal (invariant culture)
                        if (decimal.TryParse(rawValue, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal numericValue))
                        {
                            // Write the parsed decimal value
                            writer.WriteLine($"{numberField.Name}: {numericValue}");

                            // Example calculation: double the value
                            decimal doubled = numericValue * 2;
                            writer.WriteLine($"  *2 = {doubled}");
                        }
                        else
                        {
                            // Report parsing failure
                            writer.WriteLine($"{numberField.Name}: unable to parse \"{rawValue}\"");
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Parsing completed. Results saved to '{outputPath}'.");
    }
}