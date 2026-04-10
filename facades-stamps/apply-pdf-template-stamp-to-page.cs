using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // PDF to be stamped
        const string templatePdfPath = "template.pdf";  // PDF page used as stamp
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath) || !File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine("Input or template file not found.");
            return;
        }

        // Bind the source PDF and apply a stamp taken from the template PDF.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdfPath);               // source PDF

            // Create a stamp that uses page 1 of the template PDF.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindPdf(templatePdfPath, 1);             // bind template page
            stamp.IsBackground = false;                    // place on top of content
            stamp.Pages = new int[] { 3 };                 // apply only to page 3

            // Add the stamp to the document and save.
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
