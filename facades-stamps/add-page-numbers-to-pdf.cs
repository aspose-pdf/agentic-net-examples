using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileStamp works with the Facades API.
        // The constructor takes the source PDF and the destination PDF.
        // AddPageNumber uses the '#' character as a placeholder that Aspose.Pdf replaces
        // with the current page number during stamping.
        using (PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath))
        {
            fileStamp.AddPageNumber("Page #"); // "Page #" will become "Page 1", "Page 2", etc.
            fileStamp.Close(); // Finalizes and writes the output file.
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}