using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Export the form fields to a JSON string using the core ExportToJson method
            using (MemoryStream rawJsonStream = new MemoryStream())
            {
                doc.Form.ExportToJson(rawJsonStream);
                rawJsonStream.Position = 0;
                string rawJson = new StreamReader(rawJsonStream).ReadToEnd();

                // Re‑format the JSON with indented (pretty) output – the only option required
                var jsonElement = JsonDocument.Parse(rawJson).RootElement;
                var indentedOptions = new JsonSerializerOptions { WriteIndented = true };
                string indentedJson = JsonSerializer.Serialize(jsonElement, indentedOptions);

                // Write the indented JSON to the output file
                File.WriteAllText(outputJsonPath, indentedJson);
            }
        }

        Console.WriteLine($"Form fields exported to '{outputJsonPath}'.");
    }
}
