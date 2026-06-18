using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // stamped PDF
        const string stampPng  = "stamp.png";     // semi‑transparent PNG

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampPng))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPng}");
            return;
        }

        // Determine page dimensions (assumes all pages have the same size)
        double pageWidth, pageHeight;
        using (Document doc = new Document(inputPdf))
        {
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            pageWidth  = firstPage.PageInfo.Width;
            pageHeight = firstPage.PageInfo.Height;
        }

        // Prepare the stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(stampPng);                 // use the PNG image
        stamp.IsBackground = true;                 // place behind page content
        stamp.Opacity = 0.5f;                       // semi‑transparent
        stamp.SetImageSize((float)pageWidth, (float)pageHeight); // cover whole page
        stamp.SetOrigin(0, 0);                     // start at lower‑left corner

        // Apply the stamp to all pages
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);                // load source PDF
        fileStamp.AddStamp(stamp);                  // add the prepared stamp
        fileStamp.Save(outputPdf);                  // write result
        fileStamp.Close();                         // release resources

        Console.WriteLine($"Background stamp applied and saved to '{outputPdf}'.");
    }
}