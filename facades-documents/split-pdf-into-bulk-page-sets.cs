using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Folder where the split PDFs will be saved
        const string outputFolder = "SplitSets";

        // Define the page ranges (start‑end pairs) to extract
        var pageRanges = new (int start, int end)[]
        {
            (1, 5),   // pages 1‑5
            (6, 10),  // pages 6‑10
            (11, 15)  // pages 11‑15
        };

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfFileEditor does NOT implement IDisposable, so no using block
        PdfFileEditor editor = new PdfFileEditor();

        foreach (var range in pageRanges)
        {
            // Basic validation of the range
            if (range.start <= 0 || range.end < range.start)
            {
                Console.Error.WriteLine($"Invalid page range: {range.start}-{range.end}");
                continue;
            }

            // Build the output file name for this range
            string outputPath = Path.Combine(
                outputFolder,
                $"{Path.GetFileNameWithoutExtension(inputPdf)}_pages_{range.start}_{range.end}.pdf");

            try
            {
                // NOTE: In Aspose.Pdf.Facades versions prior to 26.9 the Extract overload expects
                // the page numbers before the output path. Adjusted to match that signature.
                // Extract(string sourceFile, int startPage, int endPage, string outputFile)
                editor.Extract(inputPdf, range.start, range.end, outputPath);
                Console.WriteLine($"Created: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to extract pages {range.start}-{range.end}: {ex.Message}");
            }
        }
    }
}
