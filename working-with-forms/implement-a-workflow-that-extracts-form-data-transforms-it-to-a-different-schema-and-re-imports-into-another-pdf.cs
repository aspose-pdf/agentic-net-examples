using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class FormDataTransformer
{
    static void Main()
    {
        const string sourcePdfPath = "source_form.pdf";
        const string targetPdfPath = "target_form.pdf";
        const string tempJsonPath = "form_data.json";

        // Ensure source and target PDFs exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // 1. Extract form fields from the source PDF to JSON
        using (Document sourceDoc = new Document(sourcePdfPath))
        using (FileStream exportStream = new FileStream(tempJsonPath, FileMode.Create, FileAccess.Write))
        {
            // Correct method: ExportToJson writes the form fields to the provided stream
            sourceDoc.Form.ExportToJson(exportStream);
        }

        // 2. Load the exported JSON, transform its schema, and write back to a new JSON string
        string transformedJson;
        using (FileStream readStream = new FileStream(tempJsonPath, FileMode.Open, FileAccess.Read))
        using (StreamReader reader = new StreamReader(readStream, Encoding.UTF8))
        {
            string originalJson = reader.ReadToEnd();

            // Parse JSON into a mutable DOM
            using (JsonDocument doc = JsonDocument.Parse(originalJson))
            {
                // Example transformation: rename each field by prefixing "new_"
                // (Assumes the JSON structure is a dictionary of field names to values)
                var root = doc.RootElement;
                var transformed = new Dictionary<string, JsonElement>();

                foreach (var property in root.EnumerateObject())
                {
                    string newName = "new_" + property.Name;
                    transformed[newName] = property.Value;
                }

                // Serialize the transformed dictionary back to JSON
                transformedJson = JsonSerializer.Serialize(transformed);
            }
        }

        // 3. Import the transformed JSON into the target PDF
        using (Document targetDoc = new Document(targetPdfPath))
        using (MemoryStream importStream = new MemoryStream(Encoding.UTF8.GetBytes(transformedJson)))
        {
            // Correct method: ImportFromJson reads the form fields from the provided stream
            targetDoc.Form.ImportFromJson(importStream);

            // Save the updated PDF (overwrites the original target PDF)
            targetDoc.Save(targetPdfPath);
        }

        // Cleanup temporary JSON file
        try { File.Delete(tempJsonPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine("Form data extracted, transformed, and re‑imported successfully.");
    }
}
