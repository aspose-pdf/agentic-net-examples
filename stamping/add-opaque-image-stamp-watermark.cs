using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "watermarked.pdf";
        const string stampImagePath = "logo.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Fully opaque (default is 1.0, set explicitly for clarity)
            imgStamp.Opacity = 1.0f;

            // Position the stamp – centered on each page
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the watermarked PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}