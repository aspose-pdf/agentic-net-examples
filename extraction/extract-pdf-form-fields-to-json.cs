using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Form field types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonPath  = "fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Define a rectangle that covers the entire page
            Aspose.Pdf.Rectangle pageRect = new Aspose.Pdf.Rectangle(
                0,                                   // lower‑left X
                0,                                   // lower‑left Y
                firstPage.PageInfo.Width,            // upper‑right X
                firstPage.PageInfo.Height);          // upper‑right Y

            Console.WriteLine($"Page rectangle: LLX={pageRect.LLX}, LLY={pageRect.LLY}, URX={pageRect.URX}, URY={pageRect.URY}");

            // Collect details of all form fields in the document
            var fieldDetails = new List<FieldDetail>();

            foreach (Field field in doc.Form) // doc.Form holds all form fields
            {
                FieldDetail detail = new FieldDetail {
                    Name = field.PartialName,
                    Type = field.GetType().Name,
                    Value = field.Value?.ToString(),
                    Rectangle = new RectInfo
                    {
                        LLX = field.Rect.LLX,
                        LLY = field.Rect.LLY,
                        URX = field.Rect.URX,
                        URY = field.Rect.URY
                    }
                };
                fieldDetails.Add(detail);
            }

            // Serialize the field details to a formatted JSON file
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldDetails, jsonOptions);
            File.WriteAllText(jsonPath, json);

            Console.WriteLine($"Field details saved to {jsonPath}");
        }
    }

    // Helper class representing a form field for JSON output
    public class FieldDetail
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public RectInfo Rectangle { get; set; }
    }

    // Helper class representing a rectangle in JSON
    public class RectInfo
    {
        public double LLX { get; set; }
        public double LLY { get; set; }
        public double URX { get; set; }
        public double URY { get; set; }
    }
}