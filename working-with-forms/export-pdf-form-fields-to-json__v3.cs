using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonOutput = "form_schema.json";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Configure JSON export options (optional)
            ExportFieldsToJsonOptions options = new ExportFieldsToJsonOptions
            {
                WriteIndented = true,          // Produce readable, indented JSON
                ExportPasswordValue = false    // Do not include password field values
            };

            // Export all form field definitions to a JSON file
            // This overload writes directly to the specified file path
            doc.Form.ExportToJson(jsonOutput, options);
        }

        Console.WriteLine($"Form field definitions exported to '{jsonOutput}'.");
    }
}