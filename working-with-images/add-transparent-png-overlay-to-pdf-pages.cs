using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string overlayPng = "overlay.png";    // transparent PNG to overlay
        const string outputPdf  = "output.pdf";     // result PDF

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(overlayPng))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayPng}");
            return;
        }

        // Load the PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Determine page dimensions
                double pageWidth  = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                // Define a rectangle that covers the whole page.
                // Fully qualified type avoids ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle fullPageRect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, pageHeight);

                // Add the transparent PNG on top of existing content.
                // AddImage appends the image to the page content stream, giving it the highest Z‑order.
                page.AddImage(overlayPng, fullPageRect);
            }

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Transparent overlay applied to each page. Saved as '{outputPdf}'.");
    }
}