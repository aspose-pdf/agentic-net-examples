using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with bookmarks/outlines
        const string stampImg  = "stamp.png";      // image to use as stamp
        const string outputPdf = "output.pdf";     // result PDF

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

        // Load the PDF inside a using block to ensure deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Display existing outline (bookmark) count – outlines are preserved automatically
            Console.WriteLine($"Bookmarks (outlines) before stamping: {doc.Outlines.Count}");

            // Apply the stamp to each page. A fresh ImageStamp instance is created per page
            // because a stamp cannot be reused after it has been added to a page.
            foreach (Page page in doc.Pages)
            {
                var pageStamp = new ImageStamp(stampImg)
                {
                    Background          = false,                     // overlay on top of page content
                    Opacity             = 0.5,                       // semi‑transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };
                page.AddStamp(pageStamp);
            }

            // Save the modified document; outlines (bookmarks) are retained.
            doc.Save(outputPdf);
            Console.WriteLine($"Stamped PDF saved to '{outputPdf}'");
        }
    }
}
