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
        const string stampPng  = "stamp.png";   // semi‑transparent PNG

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

        // Load the source PDF to obtain page dimensions (1‑based indexing)
        using (Document srcDoc = new Document(inputPdf))
        {
            // Assume all pages have the same size; use the first page as reference
            Page firstPage = srcDoc.Pages[1];
            double pageWidth  = firstPage.PageInfo.Width;
            double pageHeight = firstPage.PageInfo.Height;

            // Initialize the facade for stamping
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.InputFile  = inputPdf;
            fileStamp.OutputFile = outputPdf;

            // Create a stamp that uses the PNG image
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampPng);               // set the image source
            stamp.SetOrigin(0, 0);                   // lower‑left corner of the page
            stamp.SetImageSize((float)pageWidth, (float)pageHeight); // cover whole page
            stamp.Opacity = 0.5f;                    // semi‑transparent
            stamp.IsBackground = true;               // place behind page content

            // Add the stamp to all pages (Pages = null means all pages)
            stamp.Pages = null;
            fileStamp.AddStamp(stamp);

            // Finalize and write the output PDF
            fileStamp.Close();
        }

        Console.WriteLine($"Background stamp applied. Output saved to '{outputPdf}'.");
    }
}