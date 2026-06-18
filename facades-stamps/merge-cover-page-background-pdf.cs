using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string coverPath   = "cover.pdf";   // PDF to be used as background (first page)
        const string mainPath    = "main.pdf";    // Main document
        const string outputPath  = "merged.pdf";  // Resulting PDF

        // Verify input files exist
        if (!File.Exists(coverPath))
        {
            Console.Error.WriteLine($"Cover file not found: {coverPath}");
            return;
        }
        if (!File.Exists(mainPath))
        {
            Console.Error.WriteLine($"Main document not found: {mainPath}");
            return;
        }

        // Initialize PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = mainPath;   // document to which the stamp will be applied
        fileStamp.OutputFile = outputPath; // where the merged result will be saved

        // Create a stamp that uses the first page of the cover PDF as background
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(coverPath, 1);   // bind page 1 of cover.pdf
        stamp.IsBackground = true;    // place it behind existing page content

        // Apply the stamp to all pages of the main document
        fileStamp.AddStamp(stamp);

        // Finalize and save the result
        fileStamp.Close();

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}