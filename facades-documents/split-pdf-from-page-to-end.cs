using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF path, start page (1‑based), and output PDF path
        const string inputPath  = "input.pdf";
        const int    startPage  = 3;               // split from this page to the end
        const string outputPath = "output_split.pdf";

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the document first to obtain the page count and validate the start page.
            Document pdfDocument = new Document(inputPath);
            int pageCount = pdfDocument.Pages.Count;

            if (startPage < 1 || startPage > pageCount)
            {
                Console.Error.WriteLine($"Start page {startPage} is out of range. Document has {pageCount} pages.");
                return;
            }

            // PdfFileEditor does NOT implement IDisposable; instantiate directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the specified start page to the end of the document.
            // SplitToEnd uses 1‑based page indexing, matching Aspose.Pdf conventions.
            bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

            if (success)
                Console.WriteLine($"PDF successfully split. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("PDF split operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF split: {ex.Message}");
        }
    }
}
