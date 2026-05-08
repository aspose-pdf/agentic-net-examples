using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // source PDF
        const string outputPdfPath  = "output.pdf";     // result PDF
        const string footerImgPath  = "footer.png";     // decorative footer image

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(footerImgPath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImgPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Load the image once to obtain its original dimensions
            Image sampleImg = new Image();
            sampleImg.File = footerImgPath;
            double originalImgWidth  = sampleImg.BitmapSize.Width;
            double originalImgHeight = sampleImg.BitmapSize.Height;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Current page width (points)
                double pageWidth = page.PageInfo.Width;

                // Compute height that preserves the image aspect ratio
                double scaledHeight = pageWidth * originalImgHeight / originalImgWidth;

                // Define a rectangle that spans the full page width and the computed height,
                // positioned at the bottom of the page (y = 0).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,                     // left
                    0,                     // bottom
                    pageWidth,             // right (full width)
                    scaledHeight);         // top

                // Add the image to the page. The overload with (Stream, Rectangle)
                // centers the image within the rectangle while keeping its proportions.
                using (FileStream imgStream = File.OpenRead(footerImgPath))
                {
                    page.AddImage(imgStream, rect);
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Footer image added to each page. Saved as '{outputPdfPath}'.");
    }
}