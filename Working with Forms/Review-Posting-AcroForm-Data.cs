using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonOutputPath = "form_fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm of the document
            Form acroForm = doc.Form;

            // If the document has no form, inform the user
            if (acroForm == null || acroForm.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any AcroForm fields.");
                return;
            }

            Console.WriteLine($"Found {acroForm.Count} form field(s):");

            // Iterate over each field in the form
            foreach (Field field in acroForm)
            {
                // FullName is the qualified name of the field
                string name = field.FullName;

                // Value may be null for empty fields; convert to string safely
                string value = field.Value?.ToString() ?? string.Empty;

                // Print field information to console
                Console.WriteLine($"- Name: {name}, Value: {value}");
            }

            // Export all form fields to a JSON file (optional)
            // ExportToJson writes the JSON representation of the form fields
            // Note: IncludeFieldValues property does not exist in ExportFieldsToJsonOptions
            // Aspose.Pdf does not expose a direct option to exclude field values; by default, values are included
            acroForm.ExportToJson(jsonOutputPath);
            Console.WriteLine($"Form fields exported to JSON: {jsonOutputPath}");
        }
    }
}