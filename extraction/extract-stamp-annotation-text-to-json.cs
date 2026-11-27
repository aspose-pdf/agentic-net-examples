using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "FormA.pdf";
        const string outputPath = "FormA_stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
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

            // Create an anonymous object for JSON serialization
            var jsonObj = new { Text = stampText };

            // Serialize with indentation for readability
            string json = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Stamp text saved to '{outputPath}'.");
    }
}