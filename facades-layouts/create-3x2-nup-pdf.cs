using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for PageSize enum

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_3x2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so no using block is needed for it
        PdfFileEditor editor = new PdfFileEditor();

        // The current Aspose.Pdf version expects streams for MakeNUp.
        // Open the source PDF as a read‑only stream and create the destination stream.
        using (FileStream sourceStream = File.OpenRead(inputPath))
        using (FileStream destStream = File.Create(outputPath))
        {
            // 3 rows × 2 columns N‑up layout, using A4 as the target page size.
            PageSize pageSize = PageSize.A4;
            editor.MakeNUp(sourceStream, destStream, 3, 2, pageSize);
        }

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
