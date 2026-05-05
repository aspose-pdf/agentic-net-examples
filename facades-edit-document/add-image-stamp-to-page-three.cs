using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath  = "logo.png";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // -------------------------------------------------
        // Initialize the PdfFileStamp facade (no using – it is not IDisposable)
        // -------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // -------------------------------------------------
        // Create a stamp that uses the logo image
        // -------------------------------------------------
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(logoPath);                     // set image source

        // Desired size of the stamp (width x height in points)
        float stampWidth  = 100f;
        float stampHeight = 50f;
        stamp.SetImageSize(stampWidth, stampHeight);  // scale image

        // Apply the stamp only to page 3 (pages are 1‑based)
        stamp.Pages = new int[] { 3 };

        // -------------------------------------------------
        // Compute bottom‑right coordinates for page 3
        // -------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            Page page3 = doc.Pages[3];                 // 1‑based indexing
            double pageWidth  = page3.PageInfo.Width;
            double pageHeight = page3.PageInfo.Height;

            const double margin = 10.0;                // margin from edges (points)

            // Origin is measured from the lower‑left corner
            double x = pageWidth - stampWidth - margin; // right‑hand side
            double y = margin;                         // bottom

            stamp.SetOrigin((float)x, (float)y);
        }

        // -------------------------------------------------
        // Add the configured stamp and finalize the file
        // -------------------------------------------------
        fileStamp.AddStamp(stamp);
        fileStamp.Close(); // writes the output file

        Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPdf}'.");
    }
}