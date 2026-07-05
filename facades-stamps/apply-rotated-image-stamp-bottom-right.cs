using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string outputPdf  = "output.pdf";     // result PDF
        const string stampImage = "stamp.png";      // image to use as stamp
        const float  stampWidth = 100f;             // desired stamp width (points)
        const float  stampHeight = 100f;            // desired stamp height (points)
        const float  margin = 10f;                  // margin from page edges

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Determine page dimensions (assumes all pages have the same size)
        float pageWidth;
        using (Document doc = new Document(inputPdf))
        {
            pageWidth = (float)doc.Pages[1].PageInfo.Width; // explicit cast to float
        }

        // Calculate origin so that the stamp appears in the bottom‑right corner
        // Origin is measured from the lower‑left corner of the page.
        float originX = pageWidth - stampWidth - margin; // distance from left edge
        float originY = margin;                          // distance from bottom edge

        // Initialise the facade for stamping (use the non‑obsolete pattern)
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create and configure the stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(stampImage);          // use the image as stamp content
        stamp.SetImageSize(stampWidth, stampHeight);
        stamp.Rotation = 30f;                 // rotate 30 degrees
        stamp.SetOrigin(originX, originY);    // position at bottom‑right
        stamp.IsBackground = false;           // place on top of page content

        // Apply the stamp to all pages
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdf); // write the result PDF
        fileStamp.Close();
        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
