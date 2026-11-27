using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "FormH.pdf";
        const string outputPath = "FormH_stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Locate the first StampAnnotation on the page
            StampAnnotation stamp = null;
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is StampAnnotation sa)
                {
                    stamp = sa;
                    break;
                }
            }

            if (stamp == null)
            {
                Console.Error.WriteLine("No StampAnnotation found on page 1.");
                return;
            }

            // Extract the annotation's text (Contents property)
            string stampText = stamp.Contents ?? string.Empty;

            // Serialize to JSON
            var payload = new { Text = stampText };
            string json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Stamp text saved to '{outputPath}'.");
    }
}