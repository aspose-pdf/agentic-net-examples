using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Check if the document contains any form fields
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the document.");
                }
                else
                {
                    Console.WriteLine("Form field values:");
                    // Iterate over all fields in the form
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Retrieve the field name (FullName is the most complete identifier)
                        string fieldName = field.FullName ?? field.Name ?? "UnnamedField";

                        // Retrieve the field value; many field types expose a Value property
                        string fieldValue = field.Value?.ToString() ?? string.Empty;

                        Console.WriteLine($"{fieldName}: {fieldValue}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}