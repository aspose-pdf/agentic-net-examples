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
            // Load logo bytes once – StampAnnotation.Image expects a Stream
            byte[] logoBytes = File.ReadAllBytes(logoPath);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate backwards through annotations so we can safely delete items while iterating
                for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Process only WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation watermark)
                    {
                        // Preserve the existing opacity value
                        double originalOpacity = watermark.Opacity;

                        // Preserve the original rectangle (position & size)
                        Aspose.Pdf.Rectangle rect = watermark.Rect;

                        // Remove the old watermark annotation
                        page.Annotations.Delete(annIndex);

                        // Create a new StampAnnotation that will hold the logo image
                        var stamp = new StampAnnotation(page, rect)
                        {
                            Opacity = originalOpacity,
                            // Assign a MemoryStream (derived from the logo bytes) to Image
                            Image = new MemoryStream(logoBytes)
                        };

                        // Add the new image stamp to the page
                        page.Annotations.Add(stamp);
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarks replaced and saved to '{outputPdf}'.");
    }
}
