using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string watermarkImg = "watermark.png";
        const string outputPdf  = "watermarked.pdf";

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp – fully opaque (Opacity default is 1.0, set explicitly)
            ImageStamp stamp = new ImageStamp(watermarkImg);
            stamp.Opacity = 1.0;               // fully opaque
            stamp.Background = false;          // place on top of page content

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                // Resize the stamp to cover the whole page (optional – adjust as needed)
                stamp.Width  = page.PageInfo.Width;
                stamp.Height = page.PageInfo.Height;
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the watermarked PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}