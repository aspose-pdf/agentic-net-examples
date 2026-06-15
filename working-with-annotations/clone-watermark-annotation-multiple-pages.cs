using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate an existing WatermarkAnnotation (take the first one found on page 1)
            WatermarkAnnotation sourceAnno = null;
            Page firstPage = doc.Pages[1];
            foreach (Annotation ann in firstPage.Annotations)
            {
                if (ann is WatermarkAnnotation wm)
                {
                    sourceAnno = wm;
                    break;
                }
            }

            if (sourceAnno == null)
            {
                Console.Error.WriteLine("No WatermarkAnnotation found on the first page.");
                return;
            }

            // Preserve properties from the source annotation
            Aspose.Pdf.Color sourceColor = sourceAnno.Color;
            double sourceOpacity = sourceAnno.Opacity;
            string sourceContents = sourceAnno.Contents;
            Aspose.Pdf.Rectangle sourceRect = sourceAnno.Rect;

            // Define per‑page offset (example: shift right/down progressively)
            const double offsetX = 20.0;
            const double offsetY = 30.0;

            // Apply a cloned watermark to every page with adjusted position
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Compute new rectangle based on original rectangle and offsets
                double llx = sourceRect.LLX + offsetX * (i - 1);
                double lly = sourceRect.LLY + offsetY * (i - 1);
                double urx = sourceRect.URX + offsetX * (i - 1);
                double ury = sourceRect.URY + offsetY * (i - 1);
                Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create a new WatermarkAnnotation with copied properties
                WatermarkAnnotation clonedAnno = new WatermarkAnnotation(page, newRect)
                {
                    Color = sourceColor,
                    Opacity = sourceOpacity,
                    Contents = sourceContents
                };

                // Add the cloned annotation to the current page
                page.Annotations.Add(clonedAnno);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}