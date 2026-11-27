using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormD.pdf";
        const string outputJson = "FormD_allStamps.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // List to hold extracted stamp information
            var stamps = new List<StampInfo>();

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Process only StampAnnotation objects
                    if (annotation is StampAnnotation stamp)
                    {
                        // Extract relevant text properties; Title and Contents are commonly used
                        stamps.Add(new StampInfo
                        {
                            PageNumber = pageIndex,
                            Name       = stamp.Name,
                            Title      = stamp.Title,
                            Contents   = stamp.Contents
                        });
                    }
                }
            }

            // Serialize the collected stamp data to JSON
            string json = JsonSerializer.Serialize(stamps, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Extracted stamp data saved to '{outputJson}'.");
    }

    // Simple DTO for JSON serialization
    private class StampInfo
    {
        public int    PageNumber { get; set; }
        public string Name       { get; set; }
        public string Title      { get; set; }
        public string Contents   { get; set; }
    }
}