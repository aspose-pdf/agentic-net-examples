using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "watermarked.pdf";
        const string watermarkImagePath = "watermark.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a single ImageStamp instance (Aspose.Pdf.Drawing namespace)
            var stamp = new ImageStamp(watermarkImagePath)
            {
                Background = true,                     // draw behind page content
                Opacity = 0.3,                         // semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                XIndent = 0,
                YIndent = 0
            };

            // Apply the same stamp to every page
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}
