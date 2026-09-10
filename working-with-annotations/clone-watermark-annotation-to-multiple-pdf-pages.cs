using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the first WatermarkAnnotation (assumed to exist on page 1)
            WatermarkAnnotation original = null;
            foreach (Annotation ann in doc.Pages[1].Annotations)
            {
                if (ann is WatermarkAnnotation wm)
                {
                    original = wm;
                    break;
                }
            }

            if (original == null)
            {
                Console.WriteLine("No WatermarkAnnotation found on the first page.");
                doc.Save(outputPath);
                return;
            }

            // Clone the annotation to every subsequent page with adjusted positions
            for (int i = 2; i <= doc.Pages.Count; i++)
            {
                // Original rectangle of the watermark
                Aspose.Pdf.Rectangle origRect = original.Rect;

                // Example offset: shift 50 points right and 50 points up per page index
                double offsetX = 50 * (i - 1);
                double offsetY = 50 * (i - 1);

                // Create a new rectangle based on the offset
                Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                    origRect.LLX + offsetX,
                    origRect.LLY + offsetY,
                    origRect.URX + offsetX,
                    origRect.URY + offsetY);

                // Create a new WatermarkAnnotation on the target page
                WatermarkAnnotation clone = new WatermarkAnnotation(doc.Pages[i], newRect)
                {
                    // Copy visual properties from the original annotation
                    Color = original.Color,
                    Opacity = original.Opacity,
                    Contents = original.Contents,
                    // Additional properties can be copied as needed
                };

                // Add the cloned annotation to the page's annotation collection
                doc.Pages[i].Annotations.Add(clone);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark cloned to all pages. Saved as '{outputPath}'.");
    }
}