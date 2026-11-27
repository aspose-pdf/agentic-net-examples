using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormF.pdf";
        const string jsonPath = "FormF_stamp.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Pages are 1‑based; get the first page
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

                // Serialize the text to JSON
                var payload = new { Text = stampText };
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