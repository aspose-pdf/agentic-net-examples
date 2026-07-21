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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect all WatermarkAnnotations from the first page
            var firstPage = doc.Pages[1];
            var sourceWatermarks = new System.Collections.Generic.List<WatermarkAnnotation>();

            foreach (Annotation ann in firstPage.Annotations)
            {
                if (ann is WatermarkAnnotation wm)
                {
                    sourceWatermarks.Add(wm);
                }
            }

            // Duplicate each collected watermark onto every subsequent page
            for (int i = 2; i <= doc.Pages.Count; i++)
            {
                var targetPage = doc.Pages[i];
                foreach (var srcWm in sourceWatermarks)
                {
                    // Create a new watermark annotation on the target page with the same rectangle
                    WatermarkAnnotation newWm = new WatermarkAnnotation(targetPage, srcWm.Rect);

                    // Copy visual and metadata properties
                    newWm.Color = srcWm.Color;
                    newWm.Opacity = srcWm.Opacity;
                    newWm.Contents = srcWm.Contents;
                    newWm.Border = srcWm.Border;
                    newWm.Flags = srcWm.Flags;
                    newWm.Name = srcWm.Name;

                    // Add the new annotation to the page
                    targetPage.Annotations.Add(newWm);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotations duplicated to '{outputPath}'.");
    }
}