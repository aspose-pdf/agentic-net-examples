using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input_xfa.pdf";
        const string outputPath = "filled_xfa.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to inspect the XFA form and obtain field names
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains an XFA form
            if (!doc.Form.HasXfa)
            {
                Console.WriteLine("The document does not contain an XFA form.");
                return;
            }

            // Define the mapping of XFA field names to the values you want to set
            var fieldValues = new Dictionary<string, string>
            {
                { "Customer.Name", "John Doe" },
                { "Customer.Address", "123 Main St" },
                { "Order.Total", "199.99" }
            };

            // Use the Facades Form class to fill the fields
            using (Form form = new Form(inputPath))
            {
                // Iterate over the mapping and fill only existing fields
                foreach (var kvp in fieldValues)
                {
                    string fieldName = kvp.Key;
                    string fieldValue = kvp.Value;

                    // Check that the field name exists in the XFA form definition
                    if (doc.Form.XFA.FieldNames.Contains(fieldName))
                    {
                        // Fill the field with the specified value
                        form.FillField(fieldName, fieldValue);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: field '{fieldName}' not found in XFA form.");
                    }
                }

                // Save the updated PDF; the Facades Form class handles the underlying document
                form.Save(outputPath);
            }

            Console.WriteLine($"XFA form populated and saved to '{outputPath}'.");
        }
    }
}