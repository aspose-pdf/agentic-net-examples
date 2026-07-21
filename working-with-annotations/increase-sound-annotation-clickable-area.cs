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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over a copy of the annotation collection because we will modify items
                int annotationCount = page.Annotations.Count;
                for (int i = 1; i <= annotationCount; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Process only SoundAnnotation instances
                    if (ann is SoundAnnotation soundAnn)
                    {
                        // Current rectangle
                        Aspose.Pdf.Rectangle rect = soundAnn.Rect;

                        // Compute center point using Aspose.Pdf.Rectangle properties (LLX, LLY, URX, URY)
                        double centerX = (rect.LLX + rect.URX) / 2.0;
                        double centerY = (rect.LLY + rect.URY) / 2.0;

                        // Increase area by 20% → scale each dimension by sqrt(1.2)
                        double scale = Math.Sqrt(1.2);
                        double newWidth = (rect.URX - rect.LLX) * scale;
                        double newHeight = (rect.URY - rect.LLY) * scale;

                        // Build new rectangle keeping the same center
                        double newLlx = centerX - newWidth / 2.0;
                        double newLly = centerY - newHeight / 2.0;
                        double newUrx = centerX + newWidth / 2.0;
                        double newUry = centerY + newHeight / 2.0;

                        // Assign the expanded rectangle back to the annotation
                        soundAnn.Rect = new Aspose.Pdf.Rectangle(newLlx, newLly, newUrx, newUry);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted PDF saved to '{outputPath}'.");
    }
}
