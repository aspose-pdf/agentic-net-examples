using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_adjusted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate through all annotations on the page (1‑based indexing)
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Process only SoundAnnotation instances
                    if (ann is SoundAnnotation soundAnn)
                    {
                        // Current rectangle of the annotation
                        Aspose.Pdf.Rectangle oldRect = soundAnn.Rect;

                        // Center point of the rectangle
                        double centerX = (oldRect.LLX + oldRect.URX) / 2.0;
                        double centerY = (oldRect.LLY + oldRect.URY) / 2.0;

                        // Desired area increase: 20% → scale factor = sqrt(1.20)
                        const double areaScale = 1.20;
                        double dimensionScale = Math.Sqrt(areaScale);

                        // Half‑width and half‑height after scaling
                        double halfWidth  = (oldRect.URX - oldRect.LLX) / 2.0 * dimensionScale;
                        double halfHeight = (oldRect.URY - oldRect.LLY) / 2.0 * dimensionScale;

                        // Build the expanded rectangle while keeping the same centre
                        Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                            centerX - halfWidth,
                            centerY - halfHeight,
                            centerX + halfWidth,
                            centerY + halfHeight);

                        // Apply the new rectangle to the annotation
                        soundAnn.Rect = newRect;
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted SoundAnnotations saved to '{outputPath}'.");
    }
}
