using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade namespace for stamping

class Program
{
    static void Main()
    {
        const string mainPdfPath   = "main.pdf";   // PDF that will receive the cover as background
        const string coverPdfPath  = "cover.pdf";  // Single‑page PDF to be used as background
        const string outputPdfPath = "merged.pdf"; // Resulting PDF

        // Verify that input files exist
        if (!File.Exists(mainPdfPath) || !File.Exists(coverPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Initialise the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = mainPdfPath;   // source document
        fileStamp.OutputFile = outputPdfPath; // destination document

        // Create a stamp that uses the first page of the cover PDF
        Stamp stamp = new Stamp();
        stamp.BindPdf(coverPdfPath, 1); // bind page 1 of cover.pdf as stamp content
        stamp.IsBackground = true;     // place the stamp behind existing page content
        // By default stamp.Pages is null, meaning the stamp is applied to all pages

        // Add the stamp to the document and finalize
        fileStamp.AddStamp(stamp);
        fileStamp.Close(); // saves the output file

        Console.WriteLine($"Cover page merged as background. Output saved to '{outputPdfPath}'.");
    }
}