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
        const string inputPdfPath = "FormH.pdf";
        const string outputJsonPath = "FormH_allStamps.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            var stamps = new List<StampInfo>();

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Filter only StampAnnotation objects
                    if (annot is StampAnnotation stampAnnot)
                    {
                        // The visible text of a stamp is stored in the Contents property
                        string stampText = stampAnnot.Contents ?? string.Empty;

                        stamps.Add(new StampInfo
                        {
                            PageNumber = pageIndex,
                            Text = stampText
                        });
                    }
                }
            }

            // Serialize the collected stamp information to JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(stamps, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Extracted stamp texts saved to '{outputJsonPath}'.");
    }

    // Simple DTO for JSON output
    private class StampInfo
    {
        public int PageNumber { get; set; }
        public string Text { get; set; }
    }
}