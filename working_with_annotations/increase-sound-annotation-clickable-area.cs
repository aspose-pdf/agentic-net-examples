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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over a copy of the annotation collection to avoid modification issues
                int annotationCount = page.Annotations.Count;
                for (int i = 1; i <= annotationCount; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Process only SoundAnnotation instances
                    if (ann is SoundAnnotation soundAnn)
                    {
                        // Get the current rectangle
                        Aspose.Pdf.Rectangle oldRect = soundAnn.Rect;

                        // Calculate current width and height
                        double oldWidth  = oldRect.URX - oldRect.LLX;
                        double oldHeight = oldRect.URY - oldRect.LLY;

                        // Increase each dimension by 10% (approx. 20% area increase)
                        double newWidth  = oldWidth  * 1.10;
                        double newHeight = oldHeight * 1.10;

                        // Keep the rectangle centered around the original center
                        double centerX = (oldRect.LLX + oldRect.URX) / 2.0;
                        double centerY = (oldRect.LLY + oldRect.URY) / 2.0;

                        double newLLX = centerX - newWidth  / 2.0;
                        double newLLY = centerY - newHeight / 2.0;
                        double newURX = centerX + newWidth  / 2.0;
                        double newURY = centerY + newHeight / 2.0;

                        // Assign the expanded rectangle back to the annotation
                        soundAnn.Rect = new Aspose.Pdf.Rectangle(newLLX, newLLY, newURX, newURY);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted SoundAnnotations saved to '{outputPath}'.");
    }
}