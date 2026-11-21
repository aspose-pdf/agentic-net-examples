using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Map of PDF field names to the values that should be assigned
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Date",      DateTime.Today.ToShortDateString() }
            // Add additional field/value pairs as required
        };

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains a form
            var form = doc.Form;
            if (form != null && form.Fields != null)
            {
                // Iterate over every form field in the document
                foreach (var field in form.Fields)
                {
                    // Use the field's fully qualified name to look up a value
                    if (fieldValues.TryGetValue(field.FullName, out string value))
                    {
                        // Most field types expose a Value property that accepts a string
                        field.Value = value;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with populated fields saved to '{outputPath}'.");
    }
}
