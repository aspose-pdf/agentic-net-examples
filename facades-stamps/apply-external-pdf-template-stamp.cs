using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes: PdfFileStamp, Stamp

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf      = "input.pdf";      // PDF to be stamped
        const string templatePdf   = "template.pdf";   // PDF whose page will be used as stamp
        const string outputPdf     = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }

        // Initialize the facade for stamping
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Bind the source PDF (the document that will receive the stamp)
        fileStamp.BindPdf(inputPdf);

        // Create a stamp that uses a page from the external template PDF
        Stamp stamp = new Stamp();

        // Use the first page of the template PDF as the stamp content
        // (adjust the page number if a different page is required)
        stamp.BindPdf(templatePdf, 1);

        // Apply the stamp only to page 3 of the target document
        stamp.Pages = new int[] { 3 };

        // Add the configured stamp to the facade
        fileStamp.AddStamp(stamp);

        // Save the result
        fileStamp.Save(outputPdf);

        // Release resources (PdfFileStamp does not implement IDisposable,
        // so we call Close() explicitly)
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}