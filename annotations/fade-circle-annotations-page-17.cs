using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "CirclesFaded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Verify that page 17 exists (pages are 1‑based)
                if (doc.Pages.Count < 17)
                {
                    Console.Error.WriteLine("The document has fewer than 17 pages.");
                    return;
                }

                Page page = doc.Pages[17];

                // Iterate over annotations and adjust opacity for CircleAnnotations
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is CircleAnnotation circle)
                    {
                        circle.Opacity = 0.3f; // Set desired opacity
                    }
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Circles faded and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}