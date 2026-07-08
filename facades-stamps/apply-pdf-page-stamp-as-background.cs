using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // PDF to be stamped
        const string stampPdf   = "stamp.pdf";   // PDF containing the page to use as stamp
        const string outputPdf  = "output.pdf";  // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampPdf))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp file not found: {stampPdf}");
            return;
        }

        // Initialize the facade and bind the source PDF.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);               // Load the document to be processed

        // Create a stamp that uses the first page of the stamp PDF.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(stampPdf, 1);                // Use page 1 of stampPdf as stamp content
        stamp.IsBackground = true;                // Place stamp behind page content

        // By default stamp.Pages == null, so it will be applied to all pages.
        fileStamp.AddStamp(stamp);                 // Add the configured stamp

        // Save the result and release resources.
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}