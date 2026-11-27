using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string exportFolder = "Exports";
        const string outputFileName = "form_fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the Exports subfolder exists
        Directory.CreateDirectory(exportFolder);
        string jsonPath = Path.Combine(exportFolder, outputFileName);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Export all form fields to JSON using default options
                doc.Form.ExportToJson(jsonPath);
            }

            Console.WriteLine($"Form fields exported to: {jsonPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}