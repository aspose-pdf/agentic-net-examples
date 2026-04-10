using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_footer.pdf";
        const string footerImg = "footer.png";   // path to the footer image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(footerImg))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImg}");
            return;
        }

        // Load the PDF document (using the recommended load pattern)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Determine a rectangle at the bottom of the page.
                // Here we use 10 % of the page height for the footer height.
                double footerHeight = page.Rect.Height * 0.10;
                double pageWidth    = page.Rect.Width;

                // Rectangle coordinates: (llx, lly, urx, ury)
                // Place the rectangle at the bottom (y = 0) spanning the full width.
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                    0,                     // left
                    0,                     // bottom
                    pageWidth,             // right
                    footerHeight           // top
                );

                // Add the image to the page within the rectangle.
                // The image will be scaled proportionally to fit the rectangle.
                page.AddImage(footerImg, footerRect);
            }

            // Save the modified PDF (standard Save without extra options writes PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPdf}'.");
    }
}