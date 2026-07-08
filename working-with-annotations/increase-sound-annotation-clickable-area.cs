using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_adjusted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Annotations collection also uses 1‑based indexing
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Process only SoundAnnotation instances
                    if (ann is SoundAnnotation soundAnn)
                    {
                        // Original rectangle
                        Aspose.Pdf.Rectangle oldRect = soundAnn.Rect;

                        // Compute center point
                        double centerX = (oldRect.LLX + oldRect.URX) / 2.0;
                        double centerY = (oldRect.LLY + oldRect.URY) / 2.0;

                        // Increase width and height by 20%
                        double newWidth  = (oldRect.URX - oldRect.LLX) * 1.20;
                        double newHeight = (oldRect.URY - oldRect.LLY) * 1.20;

                        // Build the expanded rectangle, keeping the same center
                        double newLLX = centerX - newWidth  / 2.0;
                        double newLLY = centerY - newHeight / 2.0;
                        double newURX = centerX + newWidth  / 2.0;
                        double newURY = centerY + newHeight / 2.0;

                        // Assign the new rectangle back to the annotation
                        soundAnn.Rect = new Aspose.Pdf.Rectangle(newLLX, newLLY, newURX, newURY);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted PDF saved to '{outputPath}'.");
    }
}