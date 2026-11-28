using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_4x4_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        var editor = new PdfFileEditor();

        // Make a 4×4 N‑up layout. The overload takes an array of source files,
        // the destination path and a flag for landscape orientation.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // A5 portrait
        editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        // Adjust the page size of the generated PDF to A5.
        var outDoc = new Document(outputPath);
        foreach (Page page in outDoc.Pages)
        {
            // SetPageSize expects width and height as doubles, not a PageSize enum.
            page.SetPageSize(PageSize.A5.Width, PageSize.A5.Height);
        }
        outDoc.Save(outputPath);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
