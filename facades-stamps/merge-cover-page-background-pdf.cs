using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string coverPath = "cover.pdf";   // PDF containing the cover page
        const string mainPath  = "main.pdf";    // Main document to which the cover will be added
        const string outputPath = "merged.pdf"; // Resulting PDF

        // Verify that input files exist
        if (!File.Exists(coverPath) || !File.Exists(mainPath))
        {
            Console.Error.WriteLine("Cover or main PDF file not found.");
            return;
        }

        // Initialize the PdfFileStamp facade.
        // InputFile and OutputFile are obsolete but still functional for this operation.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile = mainPath;   // Document that will receive the stamp
        fileStamp.OutputFile = outputPath; // Destination file

        // Create a stamp that uses the first page of the cover PDF.
        Stamp stamp = new Stamp();
        stamp.BindPdf(coverPath, 1);      // Bind the cover page as stamp content
        stamp.IsBackground = true;       // Place the stamp behind existing page content

        // Add the stamp to the document. By default it applies to all pages.
        fileStamp.AddStamp(stamp);

        // Finalize and write the output file.
        fileStamp.Close();

        Console.WriteLine($"Cover merged as background and saved to '{outputPath}'.");
    }
}