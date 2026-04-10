using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";   // PDF to be stamped
        const string stampPdf   = "stamp.pdf";   // PDF whose first page will be used as stamp
        const string outputPdf  = "output.pdf";  // Resulting PDF

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

        // Initialize the facade for stamping
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Load the target PDF
        fileStamp.BindPdf(inputPdf);

        // Create a stamp that uses the first page of the stamp PDF
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(stampPdf, 1);   // page numbers are 1‑based
        stamp.IsBackground = true;   // place stamp behind page content

        // Apply the stamp to all pages (Pages = null means all pages)
        // No need to set stamp.Pages because null is default
        fileStamp.AddStamp(stamp);

        // Save the result
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}