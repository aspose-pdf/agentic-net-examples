using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "source.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Define bulk page sets as start‑end pairs with their output file names
        // Example: split pages 1‑5 into Part1.pdf, 6‑10 into Part2.pdf, etc.
        var pageRanges = new (int Start, int End, string Output)[]
        {
            (1, 5, "Part1.pdf"),
            (6, 10, "Part2.pdf"),
            (11, 15, "Part3.pdf")
            // Add more ranges as needed
        };

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        foreach (var (start, end, output) in pageRanges)
        {
            // Validate page numbers (Aspose.Pdf uses 1‑based indexing)
            if (start < 1 || end < start)
            {
                Console.Error.WriteLine($"Invalid range: {start}-{end}");
                continue;
            }

            try
            {
                // Extract the specified page range into a new PDF file
                // Correct argument order: source, startPage, endPage, outputFile
                editor.Extract(inputPdf, start, end, output);
                Console.WriteLine($"Created '{output}' from pages {start}‑{end}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to extract pages {start}-{end}: {ex.Message}");
            }
        }
    }
}
