using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string coverPath   = "cover.pdf";   // PDF page to use as background
        const string mainPath    = "document.pdf"; // Main PDF document
        const string outputPath  = "merged.pdf";

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

        // Bind the main document (input) and specify output file
        fileStamp.BindPdf(mainPath);
        fileStamp.OutputFile = outputPath; // deprecated but functional; alternatively use Save later

        // Create a stamp from the first page of the cover PDF
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(coverPath, 1);   // use page 1 of cover as stamp
        stamp.IsBackground = true;    // place it behind existing content

        // Add the stamp to the facade
        fileStamp.AddStamp(stamp);

        // Save the result and release resources
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}