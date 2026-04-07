using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg  = "logo.png";

        // Percentage offsets (0.0 – 1.0) relative to page size
        const double percentFromLeft   = 0.10; // 10% from the left edge
        const double percentFromBottom = 0.15; // 15% from the bottom edge

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the source PDF (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Optional: set stamp size (in points). If not set, original image size is used.
            // imgStamp.Width  = 100;   // example width
            // imgStamp.Height = 50;    // example height

            // Apply the stamp to each page, positioning it using percentage offsets
            foreach (Page page in doc.Pages)
            {
                // Page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Compute absolute coordinates from percentages
                imgStamp.XIndent = pageWidth  * percentFromLeft;
                imgStamp.YIndent = pageHeight * percentFromBottom;

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}