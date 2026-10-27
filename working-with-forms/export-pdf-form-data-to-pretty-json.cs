using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Forms;        // For Form handling

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF containing form fields
        const string outputJson = "formData.json";    // Destination JSON file
        const string tempJson = "tempFormData.json"; // Intermediate file (raw export)

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        // Load the PDF inside a using block to ensure deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Export the form data to a temporary JSON file using the default (compact) format
            doc.Form.ExportToJson(tempJson);
        }

        // Read the raw JSON, re‑serialize it with indentation for pretty‑printing, and write the final file
        string rawJson = File.ReadAllText(tempJson);
        using (JsonDocument doc = JsonDocument.Parse(rawJson))
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string prettyJson = JsonSerializer.Serialize(doc.RootElement, options);
            File.WriteAllText(outputJson, prettyJson);
        }

        // Clean up the temporary file
        if (File.Exists(tempJson))
        {
            File.Delete(tempJson);
        }

        Console.WriteLine($"Form data exported to '{outputJson}' with pretty‑printing.");
    }
}
