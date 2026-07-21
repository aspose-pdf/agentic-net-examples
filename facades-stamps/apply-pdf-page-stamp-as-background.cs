using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF to be stamped
        const string stampPdf  = "stamp.pdf";   // PDF whose first page will be used as stamp
        const string outputPdf = "output.pdf";  // Resulting PDF

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

        // ---------- Lifecycle: create ----------
        PdfFileStamp fileStamp = new PdfFileStamp();

        // ---------- Lifecycle: load ----------
        // Bind the source PDF that will receive the stamp
        fileStamp.BindPdf(inputPdf);

        // Create a stamp that uses the first page of the stamp PDF
        Stamp stamp = new Stamp();
        stamp.BindPdf(stampPdf, 1);   // page number is 1‑based
        stamp.IsBackground = true;   // place stamp behind page content
        stamp.Pages = null;           // null means all pages are affected

        // ---------- Apply stamp ----------
        fileStamp.AddStamp(stamp);

        // ---------- Lifecycle: save ----------
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}