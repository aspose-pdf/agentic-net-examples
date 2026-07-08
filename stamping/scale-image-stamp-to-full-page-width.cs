using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stampImage.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Load the image to obtain its original dimensions
            Image tempImg = new Image();
            tempImg.File = imagePath;
            // BitmapSize returns the size in points (1/72 inch)
            var imgSize = tempImg.BitmapSize;
            double imgOriginalWidth  = imgSize.Width;
            double imgOriginalHeight = imgSize.Height;

            // Iterate over all pages and add a full‑width image stamp
            foreach (Page page in pdfDoc.Pages)
            {
                // Page width in points
                double pageWidth = page.PageInfo.Width;

                // Compute scaling factor to fit the page width
                double scale = pageWidth / imgOriginalWidth;
                double scaledHeight = imgOriginalHeight * scale;

                // Create the image stamp
                ImageStamp imgStamp = new ImageStamp(imagePath)
                {
                    // Set the stamp width to the page width
                    Width = pageWidth,
                    // Set the height preserving aspect ratio
                    Height = scaledHeight,
                    // Position the stamp at the bottom of the page (YIndent = 0)
                    YIndent = 0,
                    // Optional: make the stamp appear behind the page content
                    Background = true,
                    // Optional: set opacity (0.0 – 1.0)
                    Opacity = 1.0
                };

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}