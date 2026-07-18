using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "formData.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Validate required fields
            bool allValid = true;

            // Iterate over the form fields collection (Aspose.Pdf.Forms)
            foreach (Field field in doc.Form.Fields)
            {
                // Only check fields that are marked as required
                if (field.Required)
                {
                    // Field.Value holds the current value; treat null/whitespace as empty
                    string fieldValue = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(fieldValue))
                    {
                        Console.Error.WriteLine($"Required field '{field.PartialName}' is empty.");
                        allValid = false;
                    }
                }
            }

            if (!allValid)
            {
                Console.Error.WriteLine("Form validation failed. Export aborted.");
                return;
            }

            // All required fields are filled – export form data to JSON
            doc.Form.ExportToJson(outputJson);
            Console.WriteLine($"Form data exported successfully to '{outputJson}'.");
        }
    }
}
