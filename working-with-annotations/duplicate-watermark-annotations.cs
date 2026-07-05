using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Collect all WatermarkAnnotations on the first page
            var firstPageWatermarks = firstPage.Annotations
                .Where(a => a is WatermarkAnnotation)
                .Cast<WatermarkAnnotation>()
                .ToList();

            // No watermarks to copy – exit early
            if (firstPageWatermarks.Count == 0)
            {
                Console.WriteLine("No WatermarkAnnotations found on the first page.");
                doc.Save(outputPath);
                return;
            }

            // Duplicate the collected watermarks onto every subsequent page
            for (int pageIndex = 2; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page targetPage = doc.Pages[pageIndex];

                foreach (WatermarkAnnotation srcWa in firstPageWatermarks)
                {
                    // Preserve the rectangle defining the annotation position
                    Aspose.Pdf.Rectangle rect = srcWa.Rect;

                    // Create a new WatermarkAnnotation on the target page
                    WatermarkAnnotation newWa = new WatermarkAnnotation(targetPage, rect)
                    {
                        // Copy visual and metadata properties
                        Color    = srcWa.Color,
                        Opacity  = srcWa.Opacity,
                        Contents = srcWa.Contents,
                        Name     = srcWa.Name,
                        Flags    = srcWa.Flags
                    };

                    // Add the new annotation to the page
                    targetPage.Annotations.Add(newWa);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"WatermarkAnnotations duplicated to all pages. Saved as '{outputPath}'.");
    }
}