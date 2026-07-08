using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and ensure proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export all form fields to a JSON file
            // The ExportToJson method writes the JSON representation of the form fields
            pdfDocument.Form.ExportToJson(outputJsonPath);
        }

        // Optional: read the generated JSON and output it to the console
        if (File.Exists(outputJsonPath))
        {
            string jsonContent = File.ReadAllText(outputJsonPath);
            Console.WriteLine(jsonContent);
        }
        else
        {
            Console.Error.WriteLine("Failed to create JSON output.");
        }
    }
}