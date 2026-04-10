using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";          // source PDF with form fields
        const string outputJson = "form_data.json";   // destination for exported JSON

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Validate required fields
            bool allFieldsValid = true;
            List<string> emptyRequiredFields = new List<string>();

            // Iterate over the form fields collection (Field objects, not WidgetAnnotation)
            foreach (Field field in doc.Form.Fields)
            {
                // Field.Required indicates a mandatory field (Aspose.Pdf.Forms.Field)
                if (field.Required)
                {
                    // Field value can be null, empty string, or whitespace
                    string value = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        allFieldsValid = false;
                        emptyRequiredFields.Add(field.PartialName ?? "(unnamed)");
                    }
                }
            }

            if (!allFieldsValid)
            {
                Console.Error.WriteLine("The following required fields are empty:");
                foreach (string name in emptyRequiredFields)
                {
                    Console.Error.WriteLine($"- {name}");
                }
                Console.Error.WriteLine("Export aborted to prevent incomplete submission.");
                return;
            }

            // All required fields have values – export form data to JSON
            // ExportToJson(string, ExportFieldsToJsonOptions) is available on Form
            doc.Form.ExportToJson(outputJson, null);
            Console.WriteLine($"Form data successfully exported to '{outputJson}'.");
        }
    }
}
