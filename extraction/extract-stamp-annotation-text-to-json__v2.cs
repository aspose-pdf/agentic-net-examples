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
        const string inputPdfPath = "FormA.pdf";
        const string outputJsonPath = "FormA_allStamps.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Collect all stamp texts with their page numbers
        List<StampInfo> stamps = new List<StampInfo>();

        // Document must be wrapped in a using block (lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Pages are 1‑based (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // We are interested only in StampAnnotation objects
                    if (annot is StampAnnotation stamp)
                    {
                        // The visible text of a stamp is stored in the Contents property
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

        // Serialize the collected data to pretty‑printed JSON
        string json = JsonSerializer.Serialize(stamps, new JsonSerializerOptions { WriteIndented = true });

        // Write JSON to the output file
        File.WriteAllText(outputJsonPath, json);

        Console.WriteLine($"Extracted {stamps.Count} stamp(s) to '{outputJsonPath}'.");
    }
}