using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

namespace AsposePdfApi
{
    class Program
    {
        static void Main()
        {
            const string inputPdf = "input.pdf";
            const string jsonOutput = "form_data.json";

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"File not found: {inputPdf}");
                return;
            }

            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Access the AcroForm of the document
                Form acroForm = doc.Form;

                // If the document has no form fields, inform the user
                if (acroForm == null || acroForm.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Iterate over all fields and print their name and value
                Console.WriteLine("Form fields extracted from the PDF:");
                foreach (Field field in acroForm)
                {
                    // Most field types expose a 'PartialName' (field name) and 'Value' (field value)
                    string fieldName = field.PartialName ?? "(unnamed)";
                    string fieldValue = field.Value?.ToString() ?? string.Empty;
                    Console.WriteLine($"- {fieldName}: {fieldValue}");
                }

                // Optionally export all form data to a JSON file (uses ExportToJson method)
                // This writes a JSON representation of the form fields to the specified path.
                acroForm.ExportToJson(jsonOutput, new ExportFieldsToJsonOptions());

                Console.WriteLine($"Form data also exported to JSON file: {jsonOutput}");
            }
        }
    }
}