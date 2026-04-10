using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "form_fields.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF using the Form facade
        using (Form form = new Form(pdfPath))
        {
            // Export all form fields to a JSON file (indented for readability)
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportJson(jsonStream, true);
            }

            // Verify that the exported file contains valid JSON
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    // The root element of a form export should be a JSON object
                    if (doc.RootElement.ValueKind == JsonValueKind.Object)
                    {
                        Console.WriteLine("Export succeeded. JSON root is an object.");
                    }
                    else
                    {
                        Console.WriteLine($"Export succeeded but unexpected JSON root type: {doc.RootElement.ValueKind}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to verify JSON: {ex.Message}");
            }
        }
    }
}