using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPath = "FormD.pdf";
        const string outputPath = "FormD_page3Stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; ensure page 3 exists
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document has fewer than 3 pages.");
                return;
            }

            Page page = doc.Pages[3];
            StampAnnotation stamp = null;

            // Locate the first StampAnnotation on page 3
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is StampAnnotation s)
                {
                    stamp = s;
                    break;
                }
            }

            if (stamp == null)
            {
                Console.Error.WriteLine("No StampAnnotation found on page 3.");
                return;
            }

            // Extract the annotation's text (Contents property)
            string stampText = stamp.Contents ?? string.Empty;

            // Serialize the extracted text to JSON
            var payload = new { Text = stampText };
            string json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Stamp text saved to '{outputPath}'.");
    }
}