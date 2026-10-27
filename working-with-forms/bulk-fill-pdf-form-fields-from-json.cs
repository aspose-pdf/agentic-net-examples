using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the values to fill – keys are field names in the PDF form
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Email",     "john.doe@example.com" },
            { "Phone",     "555-1234" } // may not exist in the template
        };

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over the values to set
                foreach (var kvp in fieldValues)
                {
                    string fieldName = kvp.Key;
                    string fieldValue = kvp.Value;

                    // Attempt to retrieve the form field; if it does not exist, skip it
                    try
                    {
                        // The Form collection returns a FormField; null if not present
                        var field = doc.Form[fieldName] as TextBoxField;
                        if (field == null)
                        {
                            // Field not found or not a TextBoxField – skip gracefully
                            continue;
                        }

                        // Set the value
                        field.Value = fieldValue;
                    }
                    catch (Exception ex)
                    {
                        // Any unexpected error while processing this field is logged,
                        // but the bulk operation continues with the next field.
                        Console.Error.WriteLine($"Skipping field '{fieldName}': {ex.Message}");
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bulk fill completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Top‑level error handling
            Console.Error.WriteLine($"Operation failed: {ex.Message}");
        }
    }
}