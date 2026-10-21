using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // optional, kept for consistency

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document using the Document class (PdfFileEditor does not support AddStamp)
        Document pdfDoc = new Document(inputPath);

        // Create an ImageStamp with 50% opacity for a translucent watermark
        ImageStamp stamp = new ImageStamp(stampImagePath)
        {
            Opacity = 0.5f,               // 50% opacity (float value)
            Background = false,          // place on top of page content
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // Apply the stamp to every page by iterating over Document.Pages
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the watermarked PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
