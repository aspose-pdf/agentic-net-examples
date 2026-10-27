using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // result PDF
        const int startPage = 5;                 // first page to keep (1‑based)

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine total page count using a Document (must be disposed)
        int totalPages;
        using (Document srcDoc = new Document(inputPath))
        {
            totalPages = srcDoc.Pages.Count;
        }

        // Validate start page
        if (startPage < 1 || startPage > totalPages)
        {
            Console.Error.WriteLine($"Start page {startPage} is out of range (1‑{totalPages}).");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        // Extract pages from startPage to the end of the document
        // Correct overload: Extract(string sourceFile, int startPage, int endPage, string outputFile)
        editor.Extract(inputPath, startPage, totalPages, outputPath);

        Console.WriteLine($"Pages {startPage}-{totalPages} saved to '{outputPath}'.");
    }
}
