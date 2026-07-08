using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "template.pdf";   // PDF with form fields
        const string outputPdfPath = "filled.pdf";     // Resulting PDF
        const string jsonDataPath  = "data.json";      // JSON file: { "FieldName":"Value", ... }

        // Ensure the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load the PDF form using the Facades Form class
        using (Form form = new Form(inputPdfPath))
        {
            // Parse the JSON mapping of field names to values
            using (FileStream jsonStream = File.OpenRead(jsonDataPath))
            {
                JsonDocument jsonDoc = JsonDocument.Parse(jsonStream);
                foreach (JsonProperty prop in jsonDoc.RootElement.EnumerateObject())
                {
                    string fieldName  = prop.Name;
                    string fieldValue = prop.Value.GetString() ?? string.Empty;

                    // Fill each field – Form.FillField expects the full field name
                    bool filled = form.FillField(fieldName, fieldValue);
                    if (!filled)
                    {
                        Console.WriteLine($"Warning: field \"{fieldName}\" not found or could not be filled.");
                    }
                }
            }

            // Save the updated PDF
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }
}