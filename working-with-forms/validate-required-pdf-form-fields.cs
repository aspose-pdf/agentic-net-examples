using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "form_fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Validate required form fields
            foreach (var field in doc.Form.Fields)
            {
                // Only check fields marked as required
                if (field.Required)
                {
                    // Most field types expose their content via the Value property.
                    // For checkboxes/radio buttons an unchecked state results in a null Value.
                    string fieldValue = field.Value?.ToString() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(fieldValue))
                    {
                        Console.Error.WriteLine(
                            $"Required field '{field.PartialName}' is empty. Export aborted.");
                        return;
                    }
                }
            }

            // All required fields have values – export form data to JSON
            doc.Form.ExportToJson(outputJson);
            Console.WriteLine($"Form data exported successfully to '{outputJson}'.");
        }
    }
}
