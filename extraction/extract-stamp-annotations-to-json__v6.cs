using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple DTO for JSON output
    private class StampInfo
    {
        public int PageNumber { get; set; }
        public string Text { get; set; }
    }

    static void Main()
    {
        const string inputPdf = "FormG.pdf";
        const string outputJson = "FormG_allStamps.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // List to hold extracted stamp texts
        var stamps = new List<StampInfo>();

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // We're interested only in StampAnnotation objects
                    if (annot is StampAnnotation stamp)
                    {
                        // StampAnnotation stores its textual content in the "Contents" property.
                        // Guard against null to avoid null reference issues.
                        string stampText = stamp.Contents ?? string.Empty;

                        stamps.Add(new StampInfo
                        {
                            PageNumber = i,
                            Text = stampText
                        });
                    }
                }
            }
        }

        // Serialize the collected data to JSON (indented for readability)
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(stamps, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Extracted {stamps.Count} stamp annotation(s) to '{outputJson}'.");
    }
}
