using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade with input and output files
        PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);

        // Optional: set the starting page number (default is 1)
        fileStamp.StartingNumber = 1;

        // Add a page‑number stamp.
        // Aspose.Pdf uses the '#' character as a placeholder that is replaced
        // with the actual page number during stamping.
        fileStamp.AddPageNumber("Page #");

        // Persist changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}