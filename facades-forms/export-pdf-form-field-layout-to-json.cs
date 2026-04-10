using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Simple DTO to hold field layout information
    private class FieldLayout
    {
        // Initialise non‑nullable properties to avoid CS8618 warnings
        public string Name { get; set; } = string.Empty;
        public int Page { get; set; }
        public float LLX { get; set; }
        public float LLY { get; set; }
        public float URX { get; set; }
        public float URY { get; set; }
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "field_layout.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                var layouts = new List<FieldLayout>();

                // Iterate over all form fields
                foreach (Field field in doc.Form)
                {
                    // Get the rectangle in page coordinates (properties are double)
                    Aspose.Pdf.Rectangle rect = field.Rect;

                    // Page index is 1‑based (field.PageIndex returns 0‑based, so add 1)
                    int pageNumber = field.PageIndex + 1;

                    layouts.Add(new FieldLayout
                    {
                        Name = field.FullName ?? string.Empty,
                        Page = pageNumber,
                        LLX = (float)rect.LLX,
                        LLY = (float)rect.LLY,
                        URX = (float)rect.URX,
                        URY = (float)rect.URY
                    });
                }

                // Serialize to indented JSON
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(layouts, jsonOptions);

                // Write JSON to file
                using (FileStream fs = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }

                Console.WriteLine($"Field layout exported to '{outputJson}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
