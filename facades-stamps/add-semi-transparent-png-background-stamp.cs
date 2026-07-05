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
        const string pngImage  = "background.png"; // semi‑transparent PNG

        // Validate files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pngImage))
        {
            Console.Error.WriteLine($"Image file not found: {pngImage}");
            return;
        }

        // Load the PDF to obtain page dimensions (first page is representative)
        double pageWidth, pageHeight;
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page firstPage = doc.Pages[1];
            pageWidth  = firstPage.PageInfo.Width;
            pageHeight = firstPage.PageInfo.Height;
        }

        // Configure the stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(pngImage);          // use the PNG as stamp content
        stamp.IsBackground = true;         // place it behind page content
        stamp.Opacity = 0.5f;               // semi‑transparent (0.0 – 1.0)
        stamp.SetOrigin(0, 0);              // lower‑left corner of the page
        stamp.SetImageSize((float)pageWidth, (float)pageHeight); // cover whole page

        // Apply the stamp to all pages using PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;
        fileStamp.AddStamp(stamp);
        fileStamp.Close(); // saves and releases resources

        Console.WriteLine($"Background stamp applied. Output saved to '{outputPdf}'.");
    }
}