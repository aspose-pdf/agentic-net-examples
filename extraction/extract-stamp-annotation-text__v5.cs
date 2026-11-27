using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "FormE.pdf";
        const string outputPath = "FormE_stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
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
                    Console.WriteLine("No StampAnnotation found on page 1.");
                    return;
                }

                // Extract the annotation's text (Contents property)
                string stampText = stamp.Contents ?? string.Empty;

                // Serialize the extracted text to JSON
                var jsonObj = new { Text = stampText };
                string json = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Stamp text saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}