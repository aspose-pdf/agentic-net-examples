using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "fields_report.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Define the custom rectangle (coordinates in points)
        // Replace with the actual rectangle you need
        Aspose.Pdf.Rectangle customRect = new Aspose.Pdf.Rectangle(
            llx: 100,   // lower‑left X
            lly: 200,   // lower‑left Y
            urx: 400,   // upper‑right X
            ury: 600    // upper‑right Y
        );

        // List to hold the result objects
        var fieldInfos = new List<object>();

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
            }
            else
            {
                // Iterate over all form fields (1‑based indexing is not required here because Fields is a collection)
                foreach (Field field in doc.Form.Fields)
                {
                    // Some field types may not have a Rect; skip those safely
                    if (field.Rect == null)
                        continue;

                    // Simple overlap test: field rectangle intersects the custom rectangle
                    bool overlaps =
                        field.Rect.LLX < customRect.URX && field.Rect.URX > customRect.LLX &&
                        field.Rect.LLY < customRect.URY && field.Rect.URY > customRect.LLY;

                    if (overlaps)
                    {
                        // FullName may be null for some fields; use PartialName as fallback
                        string fullName = field.FullName ?? field.PartialName ?? string.Empty;

                        // Retrieve the field value as string (handles different field types)
                        string value = field.Value?.ToString() ?? string.Empty;

                        fieldInfos.Add(new { FullName = fullName, Value = value });
                    }
                }
            }
        }

        // Serialize the list to JSON (indented for readability)
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(fieldInfos, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJsonPath, json);
        Console.WriteLine($"JSON report written to '{outputJsonPath}'.");
    }
}