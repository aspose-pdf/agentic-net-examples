using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "FormB.pdf";
        const string jsonPath  = "FormB_page3Stamp.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; page 3 is accessed with index 3
            Page page = doc.Pages[3];

            // Find the first StampAnnotation on this page
            StampAnnotation stamp = null;
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

            // Extract the text (Contents) of the stamp annotation
            string stampText = stamp.Contents ?? string.Empty;

            // Prepare a simple object to serialize as JSON
            var result = new
            {
                Page = 3,
                StampText = stampText
            };

            // Serialize to JSON with indentation for readability
            string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"Stamp annotation text saved to '{jsonPath}'.");
        }
    }
}