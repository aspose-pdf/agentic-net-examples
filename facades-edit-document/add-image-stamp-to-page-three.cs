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

        // Load the PDF to obtain page dimensions (page 3)
        double pageWidth, pageHeight;
        using (Document doc = new Document(inputPdf))
        {
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            Page page = doc.Pages[3]; // 1‑based indexing
            pageWidth  = page.PageInfo.Width;
            pageHeight = page.PageInfo.Height;
        }

        // Desired stamp size (in points)
        const double stampWidth  = 100; // adjust as needed
        const double stampHeight = 50;  // adjust as needed
        const double margin      = 10; // distance from page edges

        // Calculate origin for bottom‑right placement
        double xOrigin = pageWidth  - stampWidth - margin; // from left
        double yOrigin = margin;                         // from bottom

        // Prepare the stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(logoPath);               // set the image
        stamp.SetImageSize((float)stampWidth, (float)stampHeight);
        stamp.SetOrigin((float)xOrigin, (float)yOrigin);
        stamp.IsBackground = false;              // place in front of content
        stamp.Pages = new int[] { 3 };            // apply only to page 3

        // Apply the stamp using PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);              // load source PDF
        fileStamp.AddStamp(stamp);                // add the configured stamp
        fileStamp.Save(outputPdf);                // write result
        fileStamp.Close();                        // release resources

        Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPdf}'.");
    }
}