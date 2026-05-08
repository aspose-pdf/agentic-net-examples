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
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "stamps_text.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document using the recommended using pattern
        using (Document doc = new Document(inputPdfPath))
        {
            // Collect text from all StampAnnotation objects in the document
            List<string> stampTexts = new List<string>();

            // Pages are 1‑based indexed
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Only process StampAnnotation instances
                    if (ann is StampAnnotation stampAnn)
                    {
                        // The textual content of a stamp is stored in the Contents property
                        if (!string.IsNullOrEmpty(stampAnn.Contents))
                        {
                            stampTexts.Add(stampAnn.Contents);
                        }
                    }
                }
            }

            // Serialize the collected strings to a JSON array
            string json = JsonSerializer.Serialize(stampTexts, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the output file
            File.WriteAllText(outputJsonPath, json);
        }

        Console.WriteLine($"Extracted stamp texts saved to '{outputJsonPath}'.");
    }
}