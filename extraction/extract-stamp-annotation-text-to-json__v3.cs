using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "FormB.pdf";
        const string outputPath = "FormB_stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

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
                Console.Error.WriteLine("No StampAnnotation found on page 1.");
                return;
            }

            // Extract the text (Contents) of the stamp annotation
            string stampText = stamp.Contents ?? string.Empty;

            // Prepare a simple JSON object
            var jsonObj = new { text = stampText };
            string json = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Stamp text saved to '{outputPath}'.");
        }
    }
}