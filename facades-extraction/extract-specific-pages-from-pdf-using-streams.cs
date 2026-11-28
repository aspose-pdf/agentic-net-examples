using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stream_array.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Pages to extract – Aspose.Pdf uses 1‑based indexing
        int[] pagesToExtract = new int[] { 5, 10, 15, 20 };

        // Open the source and destination streams
        using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream destStream   = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it normally
            PdfFileEditor editor = new PdfFileEditor();

            // Extract the specified pages from the source stream into the destination stream
            // Signature: Extract(Stream sourcePdf, int[] pageNumbers, Stream outputPdf)
            editor.Extract(sourceStream, pagesToExtract, destStream);
        }

        Console.WriteLine($"Pages {string.Join(",", pagesToExtract)} extracted to '{outputPath}'.");
    }
}