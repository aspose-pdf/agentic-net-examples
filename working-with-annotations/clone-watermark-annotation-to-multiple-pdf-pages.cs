using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Assume the first page already contains a WatermarkAnnotation we want to clone.
            // If not present, you could create one here instead.
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
                Console.Error.WriteLine("No WatermarkAnnotation found on the first page.");
                return;
            }

            // Store the original rectangle for position calculations
            Aspose.Pdf.Rectangle origRect = original.Rect;

            // Apply clones to pages 2..N with incremental offsets
            const double offsetX = 20.0; // shift right by 20 points per page
            const double offsetY = 10.0; // shift up   by 10 points per page

            for (int i = 2; i <= doc.Pages.Count; i++)
            {
                // Clone the original annotation
                WatermarkAnnotation clone = (WatermarkAnnotation)original.Clone();

                // Compute new position based on page index
                double dx = offsetX * (i - 1);
                double dy = offsetY * (i - 1);
                clone.Rect = new Aspose.Pdf.Rectangle(
                    origRect.LLX + dx,
                    origRect.LLY + dy,
                    origRect.URX + dx,
                    origRect.URY + dy);

                // Add the cloned annotation to the current page
                doc.Pages[i].Annotations.Add(clone);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}