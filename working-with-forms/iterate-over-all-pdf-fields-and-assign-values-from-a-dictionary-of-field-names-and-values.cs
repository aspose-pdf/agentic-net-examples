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
        const string outputPdf = "filled.pdf";

        // Dictionary containing field full names and the values to assign
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Customer.Name",      "John Doe" },
            { "Customer.Address",   "123 Main St" },
            { "Order.Total",        "99.99" },
            { "Agreement.Accepted", "Yes" }
        };

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, fill fields, and save
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Try to get a matching value from the dictionary
                if (fieldValues.TryGetValue(field.FullName, out string value))
                {
                    // Assign the value to the field
                    field.Value = value;
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF fields populated and saved to '{outputPdf}'.");
    }
}