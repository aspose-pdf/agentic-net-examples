using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "target.pdf";      // PDF to be stamped
        const string disclaimerPdf = "disclaimer.pdf"; // PDF containing the legal disclaimer (first page will be used)
        const string outputPdf     = "target_with_disclaimer.pdf";

        // Verify that source files exist
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

        // Initialize the PdfFileStamp facade and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create a stamp that uses the first page of the disclaimer PDF
        Stamp stamp = new Stamp();
        stamp.BindPdf(disclaimerPdf, 1);   // bind page 1 of disclaimer.pdf as stamp content
        stamp.IsBackground = false;       // overlay on top of existing content
        stamp.Pages = new int[] { 1 };    // apply only to the first page of the target PDF

        // Add the stamp and finalize the operation
        fileStamp.AddStamp(stamp);
        fileStamp.Close(); // saves the output file

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}