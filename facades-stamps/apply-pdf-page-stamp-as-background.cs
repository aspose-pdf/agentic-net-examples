using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string stampPdf  = "stamp.pdf";   // PDF whose first page will be used as stamp
        const string outputPdf = "output.pdf";  // result PDF

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampPdf))
        {
            Console.Error.WriteLine($"Stamp file not found: {stampPdf}");
            return;
        }

        // Initialize the facade and bind the source document
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf); // load source PDF

        // Create a stamp that uses the first page of the stamp PDF
        Stamp stamp = new Stamp();
        stamp.BindPdf(stampPdf, 1);   // bind page 1 of stampPdf as stamp content
        stamp.IsBackground = true;   // place stamp behind page content
        stamp.Pages = null;           // null means all pages are affected

        // Add the stamp to the document
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}