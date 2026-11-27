using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "FormG.pdf";
        const string outputPath = "FormG_stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based; get the first page
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

                // Extract the text (Contents property holds the annotation text)
                string stampText = stamp.Contents ?? string.Empty;

                // Prepare a simple object for JSON serialization
                var jsonObj = new { Text = stampText };
                string json = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON to the output file
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