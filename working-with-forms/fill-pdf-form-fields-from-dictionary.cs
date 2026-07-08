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

        // Dictionary containing field names and the values to assign
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Email",     "john.doe@example.com" }
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, fill the fields, and save the result
        using (Document doc = new Document(inputPath))
        {
            // Iterate over every form field in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Use the fully qualified field name as the key in the dictionary
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

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}