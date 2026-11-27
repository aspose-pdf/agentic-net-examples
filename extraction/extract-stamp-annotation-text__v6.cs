using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "FormE.pdf";
        const string outputPath = "FormE_page3Stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            const int targetPageNumber = 3;
            if (doc.Pages.Count < targetPageNumber)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages; page {targetPageNumber} does not exist.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];

            // Collect the text (Contents) of all StampAnnotation objects on the page
            List<string> stampTexts = new List<string>();
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is StampAnnotation stampAnnot)
                {
                    // The textual content of a stamp annotation is stored in the Contents property
                    if (!string.IsNullOrEmpty(stampAnnot.Contents))
                        stampTexts.Add(stampAnnot.Contents);
                }
            }

            // Prepare an anonymous object for JSON serialization
            var result = new
            {
                Page = targetPageNumber,
                StampCount = stampTexts.Count,
                Stamps = stampTexts
            };

            // Serialize to JSON with indentation for readability
            string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the output file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Extracted {stampTexts.Count} stamp annotation(s) from page {targetPageNumber} and saved to '{outputPath}'.");
        }
    }
}