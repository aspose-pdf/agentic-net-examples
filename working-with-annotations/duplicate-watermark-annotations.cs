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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document must contain at least two pages.");
                return;
            }

            // Collect all WatermarkAnnotations from the first page
            Page firstPage = doc.Pages[1];
            List<WatermarkAnnotation> sourceWatermarks = new List<WatermarkAnnotation>();

            for (int i = 1; i <= firstPage.Annotations.Count; i++)
            {
                Annotation ann = firstPage.Annotations[i];
                if (ann is WatermarkAnnotation wm)
                {
                    sourceWatermarks.Add(wm);
                }
            }

            // Duplicate each collected watermark onto every subsequent page
            for (int pageIndex = 2; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page targetPage = doc.Pages[pageIndex];

                foreach (WatermarkAnnotation srcWm in sourceWatermarks)
                {
                    // Create a new watermark annotation on the target page with the same rectangle
                    WatermarkAnnotation dupWm = new WatermarkAnnotation(targetPage, srcWm.Rect);

                    // Copy relevant visual and metadata properties
                    dupWm.Color   = srcWm.Color;
                    dupWm.Opacity = srcWm.Opacity;
                    dupWm.Contents = srcWm.Contents;
                    dupWm.Border   = srcWm.Border;
                    dupWm.Flags    = srcWm.Flags;
                    dupWm.Name     = srcWm.Name;
                    dupWm.Height   = srcWm.Height;
                    dupWm.Width    = srcWm.Width;
                    dupWm.ZIndex   = srcWm.ZIndex;

                    // Add the duplicated annotation to the page
                    targetPage.Annotations.Add(dupWm);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotations duplicated and saved to '{outputPath}'.");
    }
}