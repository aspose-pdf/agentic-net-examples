using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the Facade Form with the source PDF.
            using (Form form = new Form(inputPdfPath))
            {
                // Create the output JSON file stream.
                using (FileStream jsonStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
                {
                    // Export all form field definitions (and values) to JSON.
                    // The second parameter 'indented' defaults to true for readable output.
                    form.ExportJson(jsonStream);
                }
            }

            Console.WriteLine($"Form fields exported to JSON: '{outputJsonPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}