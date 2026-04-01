using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with input and output files
        PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);

        // Create a stamp that uses the first page of the source PDF
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(inputPath, 1);
        stamp.IsBackground = true;               // place stamp behind page content
        stamp.Opacity = 0.5f;                    // optional: make it semi‑transparent

        // Apply the stamp to all pages of the document
        fileStamp.AddStamp(stamp);
        fileStamp.Close();

        Console.WriteLine($"Background stamp applied to all pages. Saved as '{outputPath}'.");
    }
}
