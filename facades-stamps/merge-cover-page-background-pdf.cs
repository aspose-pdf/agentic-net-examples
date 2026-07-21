using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileStamp and Stamp are in this namespace

class Program
{
    static void Main()
    {
        // Paths to the cover page PDF, the main document PDF, and the output PDF
        const string coverPath  = "cover.pdf";
        const string mainPath   = "main.pdf";
        const string outputPath = "merged.pdf";

        // Verify that input files exist
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

        // Initialize PdfFileStamp and specify input and output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = mainPath;   // document to which the stamp will be applied
        fileStamp.OutputFile = outputPath; // result file

        // Create a stamp that uses the first page of the cover PDF
        Stamp stamp = new Stamp();
        stamp.BindPdf(coverPath, 1); // bind page 1 of cover.pdf as stamp content
        stamp.IsBackground = true;  // place the cover as background on each page

        // Add the stamp to the file and finalize
        fileStamp.AddStamp(stamp);
        fileStamp.Close(); // saves the output and releases resources

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}