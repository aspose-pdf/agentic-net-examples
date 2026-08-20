using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string coverPath  = "cover.pdf";   // PDF to be used as background (cover page)
        const string mainPath   = "main.pdf";    // Main document
        const string outputPath = "merged.pdf";  // Resulting PDF

        // Verify that source files exist
        if (!File.Exists(coverPath))
        {
            Console.Error.WriteLine($"Cover file not found: {coverPath}");
            return;
        }
        if (!File.Exists(mainPath))
        {
            Console.Error.WriteLine($"Main file not found: {mainPath}");
            return;
        }

        // Initialize the facade with the main document as the target
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = mainPath;    // document to receive the stamp
        fileStamp.OutputFile = outputPath;  // where the merged PDF will be saved

        // Create a stamp that uses the first page of the cover PDF
        Stamp stamp = new Stamp();
        stamp.BindPdf(coverPath, 1);   // bind page 1 of cover.pdf as stamp content
        stamp.IsBackground = true;    // place the cover page behind existing content
        // By default stamp.Pages is null, meaning the stamp applies to all pages

        // Add the stamp to the document
        fileStamp.AddStamp(stamp);

        // Persist changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}