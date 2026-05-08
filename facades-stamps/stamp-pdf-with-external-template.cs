using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf    = "input.pdf";      // PDF to be stamped
        const string templatePdf = "template.pdf";   // External PDF used as stamp template
        const string outputPdf   = "output.pdf";    // Resulting PDF

        // Verify that source files exist
        if (!File.Exists(inputPdf) || !File.Exists(templatePdf))
        {
            Console.Error.WriteLine("Input PDF or template PDF not found.");
            return;
        }

        // -------------------------------------------------
        // 1. Create and load the target document via PdfFileStamp
        // -------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);   // Load the document to be stamped

        // -------------------------------------------------
        // 2. Create a stamp that uses a page from the template PDF
        // -------------------------------------------------
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the first page of the template PDF as the stamp content
        stamp.BindPdf(templatePdf, 1);

        // Apply the stamp only to the third page of the target PDF
        stamp.Pages = new int[] { 3 };

        // Optional: place the stamp behind existing content
        stamp.IsBackground = true;

        // -------------------------------------------------
        // 3. Add the stamp to the document and save
        // -------------------------------------------------
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdf);   // Persist changes to the output file
        fileStamp.Close();           // Release resources

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}