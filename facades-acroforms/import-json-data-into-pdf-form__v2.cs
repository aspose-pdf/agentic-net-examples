using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "template.pdf";   // PDF with form fields
        const string outputPdfPath = "filled.pdf";     // Result PDF
        const string jsonPath      = "data.json";      // JSON source

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load the JSON into a dictionary
        Dictionary<string, JsonElement> jsonData;
        using (FileStream jsonStream = File.OpenRead(jsonPath))
        {
            jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonStream);
        }

        // Initialize the Form facade (input PDF, output PDF)
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Retrieve all field names present in the PDF form
            HashSet<string> pdfFieldNames = new HashSet<string>(form.FieldNames, StringComparer.Ordinal);

            // Filter the JSON data: keep only entries whose keys exist in the PDF form
            var filteredData = new Dictionary<string, object>();
            foreach (var kvp in jsonData)
            {
                if (pdfFieldNames.Contains(kvp.Key))
                {
                    // Preserve the original JSON value (could be string, number, bool, etc.)
                    filteredData[kvp.Key] = kvp.Value.GetRawText();
                }
            }

            // Serialize the filtered dictionary back to JSON
            using (MemoryStream filteredJsonStream = new MemoryStream())
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(filteredJsonStream, new JsonWriterOptions { Indented = true }))
                {
                    writer.WriteStartObject();
                    foreach (var kvp in filteredData)
                    {
                        // Write raw JSON value without additional quoting
                        writer.WritePropertyName(kvp.Key);
                        using (JsonDocument doc = JsonDocument.Parse(kvp.Value.ToString()))
                        {
                            doc.RootElement.WriteTo(writer);
                        }
                    }
                    writer.WriteEndObject();
                }

                // Reset stream position before importing
                filteredJsonStream.Position = 0;

                // Import the filtered JSON into the PDF form; missing fields are ignored automatically
                form.ImportJson(filteredJsonStream);
            }

            // Save the updated PDF (output path was specified in the constructor)
            form.Save();
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdfPath}'.");
    }
}