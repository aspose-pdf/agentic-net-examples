using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet_stream_lr.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the page order for left and right pages in the booklet.
        // Adjust the arrays according to the actual page count of the source PDF.
        int[] leftPages  = { 4, 2, 6, 8 }; // example left‑hand pages
        int[] rightPages = { 1, 3, 5, 7 }; // example right‑hand pages

        // Open the source PDF and the destination PDF as streams.
        // Using blocks guarantee deterministic disposal of the streams.
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so it is not wrapped in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet using the specified left/right page arrays.
            editor.MakeBooklet(inputStream, outputStream, leftPages, rightPages);
        }

        Console.WriteLine($"Booklet PDF saved to '{outputPath}'.");
    }
}