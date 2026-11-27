using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputJson = "fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Configure export options: do not include password field values
                var options = new ExportFieldsToJsonOptions
                {
                    ExportPasswordValue = false
                };

                // Export all form fields to a JSON file using the correct API
                using (FileStream jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
                {
                    doc.Form.ExportToJson(jsonStream, options);
                }
            }

            Console.WriteLine($"Form fields exported to '{outputJson}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
