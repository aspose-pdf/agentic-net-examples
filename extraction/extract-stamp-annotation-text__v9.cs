using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormG.pdf";
        const string jsonPath = "FormG_page3Stamp.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                Page page = doc.Pages[3];

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
                    Console.Error.WriteLine("No StampAnnotation found on page 3.");
                    return;
                }

                // Extract the textual content of the stamp (stored in the Contents property)
                string stampText = stamp.Contents ?? string.Empty;

                // Prepare a simple JSON payload
                var payload = new { text = stampText };
                string json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText(jsonPath, json);
                Console.WriteLine($"Stamp text saved to '{jsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}