using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string jsonPath = "form_fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: using block for disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Initialize the Form facade with the loaded document
                using (Form form = new Form(doc))
                {
                    // Export all form fields to a JSON file (ExportJson writes to a stream)
                    using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                    {
                        form.ExportJson(jsonStream); // indented = true by default
                    }

                    Console.WriteLine($"Form fields exported to '{jsonPath}'.");
                }
            }

            // Verify the JSON structure by reading it back
            string jsonContent = File.ReadAllText(jsonPath);
            using (JsonDocument jsonDoc = JsonDocument.Parse(jsonContent))
            {
                Console.WriteLine("Exported JSON structure:");
                // Aspose exports a JSON object where each property is a field name
                foreach (JsonProperty prop in jsonDoc.RootElement.EnumerateObject())
                {
                    Console.WriteLine($"Field: {prop.Name}, Value: {prop.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}