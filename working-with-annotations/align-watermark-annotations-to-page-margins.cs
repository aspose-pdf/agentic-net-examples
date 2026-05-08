using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define a uniform margin (in points) to align the watermark annotations
            const double margin = 50.0;

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the page (1‑based indexing)
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Process only WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation watermark)
                    {
                        // Get the page size from MediaBox
                        Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                        // Compute new rectangle aligned with the page margins
                        double left   = mediaBox.LLX + margin;
                        double bottom = mediaBox.LLY + margin;
                        double right  = mediaBox.URX - margin;
                        double top    = mediaBox.URY - margin;

                        // Assign the new rectangle to the watermark annotation
                        watermark.Rect = new Aspose.Pdf.Rectangle(left, bottom, right, top);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotations aligned and saved to '{outputPath}'.");
    }
}