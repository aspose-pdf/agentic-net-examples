using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF
        const string stampImg  = "stamp.png";   // image to use as stamp

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

        // Load the PDF to obtain page dimensions (page 8)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least 8 pages
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("Document does not contain page 8.");
                return;
            }

            // Retrieve page 8 information
            Page page8 = doc.Pages[8];
            double pageWidth  = page8.PageInfo.Width;   // width in points (double)
            double pageHeight = page8.PageInfo.Height;  // height in points (double)

            // Define desired stamp size (adjust as needed) – Facade API expects float
            const float stampWidth  = 100f; // points
            const float stampHeight = 100f; // points

            // Compute origin so that the stamp is centered on the page
            float originX = (float)((pageWidth  - stampWidth)  / 2.0);
            float originY = (float)((pageHeight - stampHeight) / 2.0);

            // Create a stamp using the Facades API
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImg);                 // use the image as stamp content
            stamp.SetImageSize(stampWidth, stampHeight); // float parameters
            stamp.SetOrigin(originX, originY);           // float parameters
            stamp.Pages = new int[] { 8 };               // apply only to page 8

            // Apply the stamp to the document via PdfFileStamp
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdf);                // load source PDF
            fileStamp.AddStamp(stamp);                  // add the configured stamp
            fileStamp.Save(outputPdf);                  // write result
            fileStamp.Close();                          // release resources (no using)
        }

        Console.WriteLine($"Stamp centered on page 8 saved to '{outputPdf}'.");
    }
}
