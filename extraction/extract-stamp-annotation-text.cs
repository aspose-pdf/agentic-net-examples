using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "FormA.pdf";
        const string outputPath = "FormA_page3Stamp.json";

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

            if (targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];

            // Find the first StampAnnotation on the page
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
                Console.Error.WriteLine("No StampAnnotation found on page 3.");
                return;
            }

            // Extract the textual content of the annotation (Contents property)
            string stampText = stamp.Contents ?? string.Empty;

            // Prepare a simple object for JSON serialization
            var jsonObj = new { StampText = stampText };

            // Serialize to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(jsonObj, jsonOptions);

            // Write the JSON to the output file
            File.WriteAllText(outputPath, jsonString);
            Console.WriteLine($"Stamp text extracted and saved to '{outputPath}'.");
        }
    }
}