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

        // Load the PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.WriteLine("Document has only one page – nothing to duplicate.");
                doc.Save(outputPath); // save unchanged (lifecycle rule: save)
                return;
            }

            // Get all WatermarkAnnotations from the first page
            Page firstPage = doc.Pages[1];
            var watermarks = new System.Collections.Generic.List<WatermarkAnnotation>();

            foreach (Annotation ann in firstPage.Annotations)
            {
                if (ann is WatermarkAnnotation wm)
                {
                    watermarks.Add(wm);
                }
            }

            // No watermarks to copy?
            if (watermarks.Count == 0)
            {
                Console.WriteLine("No WatermarkAnnotations found on the first page.");
                doc.Save(outputPath);
                return;
            }

            // Duplicate each watermark onto every subsequent page
            for (int pageIdx = 2; pageIdx <= doc.Pages.Count; pageIdx++) // page-indexing-one-based
            {
                Page targetPage = doc.Pages[pageIdx];

                foreach (WatermarkAnnotation srcWm in watermarks)
                {
                    // Create a new watermark annotation on the target page with the same rectangle
                    WatermarkAnnotation newWm = new WatermarkAnnotation(targetPage, srcWm.Rect);

                    // Copy common visual properties
                    newWm.Color   = srcWm.Color;
                    newWm.Opacity = srcWm.Opacity;
                    newWm.Contents = srcWm.Contents;
                    newWm.Border   = srcWm.Border; // Border is a reference type; safe to reuse
                    newWm.Height   = srcWm.Height;
                    newWm.Width    = srcWm.Width;
                    newWm.ZIndex   = srcWm.ZIndex;
                    newWm.Flags    = srcWm.Flags;
                    newWm.ActiveState = srcWm.ActiveState;

                    // Add the cloned annotation to the page
                    targetPage.Annotations.Add(newWm);
                }
            }

            // Save the modified document (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"WatermarkAnnotations duplicated to all pages. Saved as '{outputPath}'.");
    }
}