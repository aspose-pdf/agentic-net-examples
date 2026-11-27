using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "FormC.pdf";
        const string outputPath = "FormC_stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
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

                // Prepare a simple object for JSON serialization
                var jsonObj = new { Text = stampText };

                // Serialize to JSON
                string json = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
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