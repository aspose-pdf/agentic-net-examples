using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string stampImg  = "stamp.png";   // image to use as stamp
        const string outputPdf = "output_centered_stamp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {stampImg}");
            return;
        }

        // ---------- Add the stamp ----------
        // PdfFileStamp does NOT implement IDisposable, so we manage its lifetime manually.
        PdfFileStamp fileStamp = new PdfFileStamp();
        try
        {
            // Bind the source PDF.
            fileStamp.BindPdf(inputPdf);

            // Create a stamp that uses an image.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImg);

            // Desired stamp size (points). Adjust as needed.
            const float stampWidth  = 100f;
            const float stampHeight = 100f;
            stamp.SetImageSize(stampWidth, stampHeight);

            // Calculate the center position for page 8.
            // PageWidth / PageHeight refer to the first page; we assume all pages share the same size.
            double pageWidthD  = fileStamp.PageWidth;
            double pageHeightD = fileStamp.PageHeight;
            float originX = (float)((pageWidthD  - stampWidth)  / 2.0);
            float originY = (float)((pageHeightD - stampHeight) / 2.0);
            stamp.SetOrigin(originX, originY);

            // Apply the stamp only to page 8 (1‑based indexing).
            stamp.Pages = new int[] { 8 };

            // Add the stamp to the document.
            fileStamp.AddStamp(stamp);

            // Save the result to the specified output file.
            fileStamp.Save(outputPdf);
        }
        finally
        {
            // Ensure resources are released.
            fileStamp.Close();
        }

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp centered on page 8 saved to '{outputPdf}'.");
    }
}
