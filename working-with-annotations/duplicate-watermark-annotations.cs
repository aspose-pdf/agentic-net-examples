using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Get the first page
            Page firstPage = doc.Pages[1];

            // Collect all WatermarkAnnotation objects from the first page
            List<WatermarkAnnotation> watermarks = new List<WatermarkAnnotation>();
            foreach (Annotation annot in firstPage.Annotations)
            {
                if (annot is WatermarkAnnotation wm)
                {
                    watermarks.Add(wm);
                }
            }

            // If there are no watermarks, just save the original document
            if (watermarks.Count == 0)
            {
                Console.WriteLine("No WatermarkAnnotations found on the first page.");
                doc.Save(outputPath);
                return;
            }

            // Duplicate each watermark onto every subsequent page
            for (int pageIdx = 2; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page targetPage = doc.Pages[pageIdx];
                foreach (WatermarkAnnotation srcWm in watermarks)
                {
                    // Create a new WatermarkAnnotation for the target page using the same rectangle
                    WatermarkAnnotation newWm = new WatermarkAnnotation(targetPage, srcWm.Rect);

                    // Copy properties that actually exist on WatermarkAnnotation
                    newWm.Contents = srcWm.Contents;      // inherited from Annotation
                    newWm.Opacity = srcWm.Opacity;
                    newWm.Color = srcWm.Color;
                    newWm.Border = srcWm.Border;
                    newWm.Flags = srcWm.Flags;
                    newWm.Name = srcWm.Name;
                    // NOTE: Title and Subtype are NOT members of WatermarkAnnotation – they belong to MarkupAnnotation.
                    // Therefore they are intentionally omitted.

                    // Add the duplicated annotation to the target page
                    targetPage.Annotations.Add(newWm);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"WatermarkAnnotations duplicated and saved to '{outputPath}'.");
    }
}
