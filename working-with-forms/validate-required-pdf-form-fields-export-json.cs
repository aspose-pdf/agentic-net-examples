using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // Form and related types

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "formdata.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Validate that every required form field has a non‑empty value
            bool allFieldsValid = true;

            // Iterate over the form fields collection (Field objects), not WidgetAnnotation
            foreach (Field field in doc.Form.Fields)
            {
                // Only check fields that are marked as required
                if (field.Required)
                {
                    // For most field types the Value property holds the user input
                    string fieldValue = field.Value?.ToString() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(fieldValue))
                    {
                        Console.Error.WriteLine($"Required field '{field.PartialName}' is empty.");
                        allFieldsValid = false;
                    }
                }
            }

            if (!allFieldsValid)
            {
                Console.Error.WriteLine("Form validation failed – export aborted.");
                return;
            }

            // All required fields are filled; export the form data to JSON
            using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            {
                doc.Form.ExportToJson(jsonStream);
            }

            Console.WriteLine($"Form data successfully exported to '{outputJsonPath}'.");
        }
    }
}
