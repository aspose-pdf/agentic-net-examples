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
        const string outputJson = "form_data.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Validate required fields
            List<string> missingFields = new List<string>();

            // Iterate over form fields (base type is Field)
            foreach (Field field in doc.Form)
            {
                // Check if the field is marked as required
                if (field.Required)
                {
                    // Field value can be null or empty string
                    string value = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        missingFields.Add(field.PartialName);
                    }
                }
            }

            if (missingFields.Count > 0)
            {
                Console.Error.WriteLine("The following required fields are empty:");
                foreach (string name in missingFields)
                {
                    Console.Error.WriteLine($"- {name}");
                }
                Console.Error.WriteLine("Export aborted to prevent incomplete submission.");
                return;
            }

            // All required fields have values; export form data to JSON
            doc.Form.ExportToJson(outputJson);
            Console.WriteLine($"Form data exported successfully to '{outputJson}'.");
        }
    }
}
