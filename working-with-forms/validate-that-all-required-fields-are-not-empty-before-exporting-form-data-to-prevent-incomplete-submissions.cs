using System;
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
            bool allRequiredFilled = true;

            // Iterate over all form fields (Field objects)
            foreach (Field field in doc.Form.Fields)
            {
                // In Aspose.Pdf the required flag is exposed via the 'Required' property,
                // not 'IsRequired'.
                if (field.Required)
                {
                    // Retrieve the field value as string (handles nulls)
                    string value = field.Value?.ToString() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Console.WriteLine($"Required field '{field.PartialName}' is empty.");
                        allRequiredFilled = false;
                    }
                }
            }

            // Export to JSON only if all required fields have values
            if (allRequiredFilled)
            {
                doc.Form.ExportToJson(outputJson);
                Console.WriteLine($"Form data exported to '{outputJson}'.");
            }
            else
            {
                Console.WriteLine("Export aborted due to incomplete required fields.");
            }
        }
    }
}
