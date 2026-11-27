using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "FormH.pdf";
        const string outputJsonPath = "FormH_page3Stamp.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            const int targetPageNumber = 3;
            if (targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];
            StampAnnotation stamp = null;

            // Search for the first StampAnnotation on the page
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

            // Extract the annotation's text (Contents property holds the visible text)
            string stampText = stamp.Contents ?? string.Empty;

            // Prepare a simple object to serialize to JSON
            var result = new
            {
                Page = targetPageNumber,
                StampText = stampText
            };

            // Serialize to JSON with indentation for readability
            string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the output file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Stamp annotation text saved to '{outputJsonPath}'.");
        }
    }
}