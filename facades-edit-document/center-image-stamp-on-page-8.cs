using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string stampImg  = "stamp.png";      // image to use as stamp
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

        // Load the PDF to obtain page dimensions (page 8)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least 8 pages
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("Document does not contain page 8.");
                return;
            }

            // Get width and height of page 8 (units are points, 1 inch = 72 points)
            Page page8 = doc.Pages[8];
            double pageWidth  = page8.PageInfo.Width;
            double pageHeight = page8.PageInfo.Height;

            // Desired stamp size (you can adjust as needed)
            double stampWidth  = 100; // points
            double stampHeight = 100; // points

            // Compute coordinates to center the stamp on the page
            double originX = (pageWidth  - stampWidth)  / 2.0;
            double originY = (pageHeight - stampHeight) / 2.0;

            // Initialize the PdfFileStamp facade and bind the source PDF
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdf);

            // Create a stamp, bind the image, set size and position, and limit it to page 8
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImg);                       // use an image as the stamp
            stamp.SetImageSize((float)stampWidth, (float)stampHeight);
            stamp.SetOrigin((float)originX, (float)originY); // position the stamp
            stamp.IsBackground = false;                     // draw on top of page content
            stamp.Pages = new int[] { 8 };                  // apply only to page 8

            // Add the stamp to the document and save
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputPdf);
            fileStamp.Close();

            Console.WriteLine($"Aspose.Pdf.Facades.Stamp centered on page 8 and saved to '{outputPdf}'.");
        }
    }
}
