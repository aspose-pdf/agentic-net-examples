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
            // Assume the watermark annotation exists on the first page
            Page sourcePage = doc.Pages[1];
            WatermarkAnnotation original = null;

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

            // Base rectangle of the original watermark
            Aspose.Pdf.Rectangle baseRect = original.Rect;

            // Apply cloned watermark to each page with an offset
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page targetPage = doc.Pages[i];

                // Skip the source page if you don't want duplicate on it
                // if (i == 1) continue;

                // Compute offset (e.g., 20 points right and 20 points up per page index)
                double offsetX = 20 * (i - 1);
                double offsetY = 20 * (i - 1);

                Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                    baseRect.LLX + offsetX,
                    baseRect.LLY + offsetY,
                    baseRect.URX + offsetX,
                    baseRect.URY + offsetY);

                // Create a new watermark annotation on the target page
                WatermarkAnnotation clone = new WatermarkAnnotation(targetPage, newRect)
                {
                    // Copy visual properties from the original
                    Color   = original.Color,
                    Opacity = original.Opacity,
                    Contents = original.Contents,
                    // You can copy additional properties as needed
                };

                // Add the cloned annotation to the page
                targetPage.Annotations.Add(clone);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark cloned and applied. Saved to '{outputPath}'.");
    }
}