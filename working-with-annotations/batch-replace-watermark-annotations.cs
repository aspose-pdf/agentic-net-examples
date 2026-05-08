using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "output.pdf";        // result PDF
        const string logoPath = "corporate_logo.png"; // new watermark image

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Iterate backwards because we may delete/replace annotations
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];

                    // Identify WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation watermarkAnn)
                    {
                        // Preserve the existing opacity
                        double originalOpacity = watermarkAnn.Opacity;

                        // Preserve the original rectangle (position & size)
                        Aspose.Pdf.Rectangle rect = watermarkAnn.Rect;

                        // Remove the old watermark annotation
                        page.Annotations.Delete(i);

                        // Create a new StampAnnotation that uses the corporate logo image
                        var stampAnn = new StampAnnotation(page, rect);

                        // Load image bytes and assign a MemoryStream (required type for StampAnnotation.Image)
                        byte[] imgBytes = File.ReadAllBytes(logoPath);
                        stampAnn.Image = new MemoryStream(imgBytes);

                        // Preserve opacity
                        stampAnn.Opacity = originalOpacity;

                        // Add the new annotation to the page
                        page.Annotations.Add(stampAnn);
                    }
                }
            }

            // Save the modified document (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarks replaced and saved to '{outputPath}'.");
    }
}
