using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for ImageStamp
using Aspose.Pdf.Drawing;   // for Image if needed

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_with_header_footer.pdf";
        const string headerImgPath  = "header.png";
        const string footerImgPath  = "footer.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(headerImgPath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImgPath}");
            return;
        }
        if (!File.Exists(footerImgPath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImgPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create an ImageStamp for the header
            ImageStamp headerStamp = new ImageStamp(headerImgPath)
            {
                // Center the image horizontally
                HorizontalAlignment = HorizontalAlignment.Center,
                // Position at the top of the page
                VerticalAlignment   = VerticalAlignment.Top,
                // Small margin from the top edge
                TopMargin           = 10,
                // Do not treat the stamp as background (it will be drawn over content)
                Background          = false,
                // Keep original image size (set Width/Height if scaling is required)
                // Width  = 0, // 0 means use image's natural width
                // Height = 0
            };

            // Create an ImageStamp for the footer
            ImageStamp footerStamp = new ImageStamp(footerImgPath)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                BottomMargin        = 10,
                Background          = false
            };

            // Apply the header and footer stamps to every page
            foreach (Page page in doc.Pages)
            {
                // Add header stamp
                page.AddStamp(headerStamp);
                // Add footer stamp
                page.AddStamp(footerStamp);
            }

            // Save the modified PDF (using the standard Save method)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with image header/footer saved to '{outputPdfPath}'.");
    }
}