using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF with form fields and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";

        // Dictionary containing field names (full names) and the values to assign
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName", "Doe" },
            { "Age", "30" }
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Iterate over all fields in the form
            foreach (Field field in form.Fields)
            {
                // Use the fully qualified field name to look up a value
                string name = field.FullName;
                if (fieldValues.TryGetValue(name, out string value))
                {
                    // Assign the value to the field
                    field.Value = value;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPath}'.");
    }
}