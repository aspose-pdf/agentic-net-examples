using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_6x6_A5.pdf";

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        var editor = new PdfFileEditor();

        // The MakeNUp overload available in recent Aspose.Pdf versions works with streams.
        // Open the source PDF for reading and the destination PDF for writing.
        using (FileStream srcStream = File.OpenRead(inputPath))
        using (FileStream dstStream = File.Create(outputPath))
        {
            // Create a 6×6 N‑up layout on A5 sized pages.
            editor.MakeNUp(srcStream, dstStream, 6, 6, PageSize.A5);
        }

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
