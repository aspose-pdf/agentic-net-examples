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
        const string outputPath = "FormC_page3Stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based; ensure page 3 exists
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("Document has fewer than 3 pages.");
                    return;
                }

                Page page3 = doc.Pages[3];
                string stampText = null;

                // Locate the first StampAnnotation on page 3
                foreach (Annotation annot in page3.Annotations)
                {
                    if (annot is StampAnnotation stamp)
                    {
                        // The visible text of a stamp is stored in the Contents property
                        stampText = stamp.Contents;
                        break;
                    }
                }

                if (stampText == null)
                {
                    Console.Error.WriteLine("No StampAnnotation found on page 3.");
                    return;
                }

                // Serialize the extracted text to JSON
                var json = JsonSerializer.Serialize(
                    new { Text = stampText },
                    new JsonSerializerOptions { WriteIndented = true });

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