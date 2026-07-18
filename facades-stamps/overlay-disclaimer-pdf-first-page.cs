using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "target.pdf";      // PDF to be stamped
        const string disclaimerPdfPath = "disclaimer.pdf"; // PDF containing the legal disclaimer (first page will be used)
        const string outputPdfPath     = "target_stamped.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(disclaimerPdfPath))
        {
            Console.Error.WriteLine($"Disclaimer PDF not found: {disclaimerPdfPath}");
            return;
        }

        // Initialize PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdfPath;   // source document
        fileStamp.OutputFile = outputPdfPath;  // result document

        // Create a stamp that uses the first page of the disclaimer PDF
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(disclaimerPdfPath, 1);   // use page 1 of disclaimer.pdf as stamp content
        stamp.IsBackground = true;            // place stamp behind existing content
        stamp.Pages = new int[] { 1 };         // apply only to the first page of the target PDF

        // Add the stamp to the document
        fileStamp.AddStamp(stamp);

        // Finalize and save
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}