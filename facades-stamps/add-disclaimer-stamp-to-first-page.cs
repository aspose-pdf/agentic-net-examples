using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf      = "target.pdf";          // PDF to be stamped
        const string disclaimerPdf = "disclaimer.pdf";      // PDF containing the legal disclaimer (first page will be used)
        const string outputPdf     = "target_with_disclaimer.pdf";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(disclaimerPdf))
        {
            Console.Error.WriteLine($"Disclaimer PDF not found: {disclaimerPdf}");
            return;
        }

        // Initialize the facade, bind the source document and configure the stamp
        PdfFileStamp fileStamp = new PdfFileStamp();
        try
        {
            // Bind the target PDF that will receive the stamp
            fileStamp.BindPdf(inputPdf);

            // Create a stamp that uses the first page of the disclaimer PDF
            Stamp stamp = new Stamp();
            stamp.BindPdf(disclaimerPdf, 1);          // use page 1 of disclaimer.pdf as stamp content
            stamp.IsBackground = false;               // place stamp on top of existing content
            stamp.Pages = new int[] { 1 };            // apply only to the first page of the target PDF

            // Add the stamp to the document
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(outputPdf);
        }
        finally
        {
            // Close releases resources; PdfFileStamp does not implement IDisposable
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}