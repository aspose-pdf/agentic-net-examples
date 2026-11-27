using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormD.pdf";
        const string jsonPath = "FormD_stamp.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
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

            // Prepare a simple object to serialize as JSON
            var jsonObject = new { text = stampText };

            // Serialize to JSON with indentation for readability
            string json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Stamp text saved to '{jsonPath}'.");
    }
}