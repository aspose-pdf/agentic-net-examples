using System;
using System.IO;
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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Assume the watermark to clone is on the first page
            Page sourcePage = doc.Pages[1];
            WatermarkAnnotation original = null;

            // Locate the first WatermarkAnnotation on the source page
            foreach (Annotation ann in sourcePage.Annotations)
            {
                if (ann is WatermarkAnnotation wm)
                {
                    original = wm;
                    break;
                }
            }

            if (original == null)
            {
                Console.Error.WriteLine("No WatermarkAnnotation found on the first page.");
                return;
            }

            // Clone the watermark to all other pages with adjusted positions
            for (int i = 2; i <= doc.Pages.Count; i++)
            {
                Page targetPage = doc.Pages[i];

                // Compute a new rectangle by offsetting the original position
                double offsetX = 20 * (i - 2); // example offset per page
                double offsetY = 10 * (i - 2);

                Aspose.Pdf.Rectangle origRect = original.Rect;
                Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                    origRect.LLX + offsetX,
                    origRect.LLY + offsetY,
                    origRect.URX + offsetX,
                    origRect.URY + offsetY);

                // Create a new WatermarkAnnotation for the target page
                WatermarkAnnotation clone = new WatermarkAnnotation(targetPage, newRect)
                {
                    // Copy visual properties from the original
                    Color   = original.Color,
                    Opacity = original.Opacity,
                    Contents = original.Contents,
                    Border   = original.Border
                };

                // Add the cloned annotation to the target page
                targetPage.Annotations.Add(clone);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark cloned and saved to '{outputPath}'.");
    }
}