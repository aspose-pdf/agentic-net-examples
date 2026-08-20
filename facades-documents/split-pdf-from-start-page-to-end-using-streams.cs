using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";
        // Page number from which to start the split (1‑based indexing)
        const int startPage = 5;
        // Path for the resulting PDF containing pages from startPage to the end
        const string outputPath = "split_end.pdf";

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Open the source and destination streams inside using blocks for deterministic disposal
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the specified start page to the end of the document.
            // The method returns true on success; we can optionally check the result.
            bool success = editor.SplitToEnd(inputStream, startPage, outputStream);

            if (success)
                Console.WriteLine($"Successfully split PDF from page {startPage} to the end. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Split operation failed.");
        }
    }
}