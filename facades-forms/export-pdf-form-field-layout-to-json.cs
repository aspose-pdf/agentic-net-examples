using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "fields_layout.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (using rule for document disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Collect layout information for each form field
            var fieldsLayout = new List<FieldLayout>();

            foreach (var field in pdfDoc.Form.Fields)
            {
                // Rectangle describing the field position on its page
                Aspose.Pdf.Rectangle rect = field.Rect;

                fieldsLayout.Add(new FieldLayout
                {
                    Name = field.FullName,
                    Page = field.PageIndex,               // 1‑based page index
                    LowerLeftX = rect.LLX,
                    LowerLeftY = rect.LLY,
                    UpperRightX = rect.URX,
                    UpperRightY = rect.URY
                });
            }

            // Serialize layout data to indented JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldsLayout, jsonOptions);
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Form field layout exported to '{outputJsonPath}'.");
    }

    // Helper class representing the JSON structure for a field
    private class FieldLayout
    {
        public string Name { get; set; }
        public int Page { get; set; }
        public double LowerLeftX { get; set; }
        public double LowerLeftY { get; set; }
        public double UpperRightX { get; set; }
        public double UpperRightY { get; set; }
    }
}