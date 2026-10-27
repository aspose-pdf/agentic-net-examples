using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "template.pdf";   // PDF with form fields
        const string jsonDataPath    = "data.json";     // JSON file containing key/value pairs
        const string outputPdfPath   = "filled_form.pdf";

        // Validate input files
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load JSON document
        JsonDocument jsonDoc;
        try
        {
            jsonDoc = JsonDocument.Parse(File.ReadAllText(jsonDataPath));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Load the PDF document
        Document pdfDocument;
        try
        {
            pdfDocument = new Document(templatePdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
            return;
        }

        // Ensure the document actually contains a form
        if (pdfDocument.Form == null || pdfDocument.Form.Fields == null)
        {
            Console.Error.WriteLine("The PDF does not contain any form fields.");
            return;
        }

        // Iterate over each property in the JSON root object and map it to a form field
        foreach (JsonProperty prop in jsonDoc.RootElement.EnumerateObject())
        {
            // Convert the JSON value to a string representation suitable for a PDF field
            string fieldValue = prop.Value.ValueKind switch
            {
                JsonValueKind.String => prop.Value.GetString() ?? string.Empty,
                JsonValueKind.Number => prop.Value.GetRawText(),
                JsonValueKind.True   => "true",
                JsonValueKind.False  => "false",
                JsonValueKind.Null   => string.Empty,
                _                    => prop.Value.GetRawText()
            };

            // Find the matching field by name (case‑sensitive)
            Field pdfField = pdfDocument.Form.Fields.FirstOrDefault(f => f.Name == prop.Name);
            if (pdfField == null)
            {
                Console.Error.WriteLine($"Warning: field '{prop.Name}' not found in the PDF form.");
                continue;
            }

            // Set the field value. Most field types expose a Value property that accepts an object.
            try
            {
                pdfField.Value = fieldValue;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Warning: could not set value for field '{prop.Name}': {ex.Message}");
            }
        }

        // Save the filled PDF
        try
        {
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save filled PDF: {ex.Message}");
        }
    }
}
