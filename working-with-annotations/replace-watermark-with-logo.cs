using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";        // result PDF
        const string logoPath = "corporate_logo.png"; // new watermark image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate backwards so we can safely remove annotations while iterating
                for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Process only WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation wmAnn)
                    {
                        // Preserve the existing opacity and rectangle
                        double originalOpacity = wmAnn.Opacity;
                        Aspose.Pdf.Rectangle rect = wmAnn.Rect;

                        // Remove the old watermark annotation
                        page.Annotations.Delete(annIndex);

                        // Create a new StampAnnotation (image based) with the same rectangle
                        var stamp = new StampAnnotation(page, rect);

                        // Load the logo image into a MemoryStream (StampAnnotation.Image expects a Stream)
                        byte[] imgBytes = File.ReadAllBytes(logoPath);
                        stamp.Image = new MemoryStream(imgBytes);
                        stamp.Opacity = originalOpacity;

                        // Add the new image stamp to the page
                        page.Annotations.Add(stamp);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarks replaced and saved to '{outputPdf}'.");
    }
}
