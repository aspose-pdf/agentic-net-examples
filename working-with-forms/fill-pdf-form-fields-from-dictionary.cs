using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "filled.pdf";

        // Dictionary containing field names (full names) and the values to assign
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName", "Doe" },
            { "Age", "30" }
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, fill fields, and save – wrapped in a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over every form field in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Use the field's full qualified name to look up a value in the dictionary
                if (fieldValues.TryGetValue(field.FullName, out string value))
                {
                    // Assign the value to the field
                    field.Value = value;
                }
            }

            // Persist the changes to a new file
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}