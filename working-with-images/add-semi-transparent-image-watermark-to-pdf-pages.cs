using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the watermark image, and the output PDF.
        const string inputPdfPath   = "input.pdf";
        const string watermarkPath  = "watermark.png";
        const string outputPdfPath  = "output.pdf";

        // Opacity value for the watermark (0.0 = fully transparent, 1.0 = fully opaque).
        // In a real scenario this could be read from a configuration file.
        const double watermarkOpacity = 0.35;

        // Validate input files.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        // NOTE: Aspose.Pdf must be added to the project via the NuGet package "Aspose.PDF".
        // Do NOT reference a local AsposePdfApi.dll file – the missing‑file build error is caused by that.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages.
            foreach (Page page in pdfDoc.Pages)
            {
                // Create an ImageStamp from the watermark image file.
                ImageStamp stamp = new ImageStamp(watermarkPath)
                {
                    Opacity = watermarkOpacity,               // Set semi‑transparent opacity.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Background = false                       // Draw over page content.
                };

                // Apply the stamp to the current page.
                page.AddStamp(stamp);
            }

            // Save the modified PDF to the output path.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}
