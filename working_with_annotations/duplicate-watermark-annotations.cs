using System;
using System.Collections.Generic;
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Collect all WatermarkAnnotations from the first page
            Page firstPage = doc.Pages[1];
            var firstPageWatermarks = new List<WatermarkAnnotation>();

            for (int i = 1; i <= firstPage.Annotations.Count; i++)
            {
                Annotation ann = firstPage.Annotations[i];
                if (ann is WatermarkAnnotation wm)
                {
                    firstPageWatermarks.Add(wm);
                }
            }

            // No watermarks to duplicate? Exit early.
            if (firstPageWatermarks.Count == 0)
            {
                Console.WriteLine("No WatermarkAnnotations found on the first page.");
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
                return;
            }

            // Duplicate each collected watermark onto every subsequent page
            for (int pageIndex = 2; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page targetPage = doc.Pages[pageIndex];

                foreach (WatermarkAnnotation srcWm in firstPageWatermarks)
                {
                    // Create a new watermark annotation with the same rectangle
                    WatermarkAnnotation newWm = new WatermarkAnnotation(targetPage, srcWm.Rect);

                    // Copy visual and content properties
                    newWm.Color   = srcWm.Color;
                    newWm.Opacity = srcWm.Opacity;
                    newWm.Border  = srcWm.Border;   // Border can be null; copying reference is acceptable
                    newWm.Contents = srcWm.Contents;

                    // Add the new annotation to the target page
                    targetPage.Annotations.Add(newWm);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
            Console.WriteLine($"All WatermarkAnnotations duplicated. Saved to '{outputPath}'.");
        }
    }
}