using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string headerImg = "header.png";         // decorative header image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(headerImg))
        {
            Console.Error.WriteLine($"Header image not found: {headerImg}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Desired header image height (adjust as needed)
                double imgHeight = 50;   // points
                double marginTop = 10;   // space from top edge

                // Compute rectangle for the header image.
                // Origin is bottom‑left, so bottom = pageHeight - imgHeight - marginTop
                double left   = 0;
                double right  = pageWidth;
                double bottom = pageHeight - imgHeight - marginTop;
                double top    = pageHeight - marginTop;

                // Fully qualified rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(left, bottom, right, top);

                // Add the image to the page within the rectangle.
                // The image will be centered and scaled proportionally.
                page.AddImage(headerImg, rect);
            }

            // Save the modified PDF (lifecycle rule: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header image added to each page. Saved as '{outputPdf}'.");
    }
}