using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string pngPath = "image.png";
        const string pdfPath = "output.pdf";

        // Verify the PNG file exists
        if (!File.Exists(pngPath))
        {
            Console.Error.WriteLine($"File not found: {pngPath}");
            return;
        }

        // Obtain image dimensions using System.Drawing.Image (width/height in points)
        double imgWidth;
        double imgHeight;
        // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image
        using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(pngPath))
        {
            // Convert pixels to points (1 point = 1/72 inch)
            imgWidth  = sysImg.Width  * 72.0 / sysImg.HorizontalResolution;
            imgHeight = sysImg.Height * 72.0 / sysImg.VerticalResolution;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a blank page
            Page page = pdfDoc.Pages.Add();

            // Match the page size to the image dimensions
            page.PageInfo.Width  = imgWidth;
            page.PageInfo.Height = imgHeight;

            // Create an Aspose.Pdf.Image paragraph and size it to fill the page
            // Fully qualify Aspose.Pdf.Image to avoid ambiguity with System.Drawing.Image
            Aspose.Pdf.Image pdfImg = new Aspose.Pdf.Image
            {
                File = pngPath,
                FixWidth = imgWidth,
                FixHeight = imgHeight,
                // Ensure the image occupies the whole page as a block element
                IsInLineParagraph = false
            };

            // Add the image to the page at the origin (0,0)
            page.Paragraphs.Add(pdfImg);

            // Save the result as a PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PNG successfully converted to PDF: {pdfPath}");
    }
}
