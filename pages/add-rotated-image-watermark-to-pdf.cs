using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for ImageStamp and related drawing classes

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";        // source PDF
        const string watermarkImg = "watermark.png"; // watermark image file
        const string outputPdf = "output.pdf";      // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(watermarkImg))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImg}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Work with the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Create an ImageStamp for the watermark
            ImageStamp stamp = new ImageStamp(watermarkImg);

            // Scale the watermark to half the page size
            double pageWidth = page.Rect.Width;
            double pageHeight = page.Rect.Height;
            stamp.Width = pageWidth / 2.0;
            stamp.Height = pageHeight / 2.0;

            // Rotate the watermark 45 degrees
            stamp.RotateAngle = 45;

            // Center the watermark on the page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            // Add the stamp to the page
            page.AddStamp(stamp);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
