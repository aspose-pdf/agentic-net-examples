using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF path and output PDF path
        const string inputPath  = "input.pdf";
        const string outputPath = "filled_output.pdf";

        // Dictionary containing field names and the values to assign
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Email",     "john.doe@example.com" },
            // add more field/value pairs as needed
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Try to get a value for the current field name from the dictionary
                if (fieldValues.TryGetValue(field.Name, out string value))
                {
                    // Assign the value to the field
                    field.Value = value;
                }
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF fields populated and saved to '{outputPath}'.");
    }
}