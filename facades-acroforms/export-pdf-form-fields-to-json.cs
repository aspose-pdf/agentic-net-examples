using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";
        const string jsonPath = "form_fields.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Bind the PDF to the Form facade
        using (Form form = new Form(pdfPath))
        {
            // Export all form fields to a JSON file
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportJson(jsonStream, indented: true);
            }

            // Verify the exported JSON structure
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    if (doc.RootElement.ValueKind != JsonValueKind.Object)
                    {
                        Console.Error.WriteLine("Invalid JSON: root element is not an object.");
                    }
                    else
                    {
                        foreach (JsonProperty prop in doc.RootElement.EnumerateObject())
                        {
                            Console.WriteLine($"{prop.Name}: {prop.Value}");
                        }
                    }
                }
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"JSON parsing error: {ex.Message}");
            }
        }

        Console.WriteLine($"Form fields exported to '{jsonPath}'.");
    }
}