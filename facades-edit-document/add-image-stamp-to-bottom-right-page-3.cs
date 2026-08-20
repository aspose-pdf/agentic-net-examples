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

        // Load the PDF to obtain the size of page 3
        using (Document doc = new Document(inputPdf))
        {
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF does not contain a third page.");
                return;
            }

            Page page3 = doc.Pages[3];
            double pageWidth  = page3.PageInfo.Width;
            double pageHeight = page3.PageInfo.Height;

            // Desired stamp dimensions (adjust as needed)
            double stampWidth  = 100; // points
            double stampHeight = 50;  // points

            // Origin for bottom‑right corner (lower‑left corner of the stamp)
            double originX = pageWidth - stampWidth; // right edge minus stamp width
            double originY = 0;                       // bottom edge

            // Create and configure the image stamp
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(logoPath);                                 // set image source
            stamp.SetImageSize((float)stampWidth, (float)stampHeight); // set size
            stamp.SetOrigin((float)originX, (float)originY);           // set position
            stamp.IsBackground = false;                               // place on top (default)
            stamp.Pages = new int[] { 3 };                            // affect only page 3

            // Apply the stamp using the PdfFileStamp facade
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.InputFile  = inputPdf;
            fileStamp.OutputFile = outputPdf;
            fileStamp.AddStamp(stamp);
            fileStamp.Close(); // finalize and save
        }

        Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPdf}'.");
    }
}