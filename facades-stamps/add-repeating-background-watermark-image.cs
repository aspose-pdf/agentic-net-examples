using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath     = "input.pdf";
        const string outputPath    = "watermarked.pdf";
        const string watermarkPath = "watermark.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        // Load the source PDF using the Document class (PdfFileMend does not support AddStamp)
        Document pdfDocument = new Document(inputPath);

        // Create a single ImageStamp instance – the same instance will be reused for every page
        ImageStamp stamp = new ImageStamp(watermarkPath)
        {
            Background = true,   // place behind existing page content
            Opacity    = 0.3,    // semi‑transparent watermark
            Zoom       = 0.5     // scale the image to 50 % of its original size
        };
        // Optional: centre the stamp on each page (you can adjust alignment/indents as needed)
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment   = VerticalAlignment.Center;

        // Apply the same stamp instance to every page
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the watermarked PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
