using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "watermarked.pdf";    // result PDF
        const string stampImg  = "watermark.png";      // watermark image

        // Verify that all required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        try
        {
            // Create an ImageStamp and set its opacity to 50%
            ImageStamp stamp = new ImageStamp(stampImg)
            {
                Opacity = 0.5 // 50% opacity for translucent effect
                // Optional: position and size can be adjusted here
                // HorizontalAlignment = HorizontalAlignment.Center,
                // VerticalAlignment   = VerticalAlignment.Center
            };

            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Apply the stamp to every page
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the watermarked PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
