using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "filled_output.pdf";

        // Example field values to assign (field full name -> value)
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Email",     "john.doe@example.com" }
            // Add more field name/value pairs as needed
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate over all form fields in the document
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Try to find a matching entry in the dictionary using the field's full name
                if (fieldValues.TryGetValue(field.FullName, out string value))
                {
                    // Assign the value to the field
                    field.Value = value;
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF fields populated and saved to '{outputPdf}'.");
    }
}