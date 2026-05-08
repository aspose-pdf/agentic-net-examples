using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "formdata.json";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Export all form fields to a JSON file.
            // The ExportToJson method writes the JSON representation of the form fields
            // directly to the specified file. No additional options are required.
            pdfDocument.Form.ExportToJson(outputJsonPath);
        }

        // Optional: read the generated JSON and output it to the console
        if (File.Exists(outputJsonPath))
        {
            string jsonContent = File.ReadAllText(outputJsonPath);
            Console.WriteLine("Extracted form fields (JSON):");
            Console.WriteLine(jsonContent);
        }
        else
        {
            Console.Error.WriteLine("Failed to create JSON output.");
        }
    }
}