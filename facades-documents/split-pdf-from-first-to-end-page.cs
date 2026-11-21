using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the last page to include in the split
        const string inputPath  = "input.pdf";
        const string outputPath = "split_output.pdf";
        const int    endPage    = 5;   // inclusive; change as needed

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Validate the requested page range using a Document (disposed via using)
        int totalPages;
        try
        {
            using (Document srcDoc = new Document(inputPath))
            {
                totalPages = srcDoc.Pages.Count;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read source PDF: {ex.Message}");
            return;
        }

        if (endPage < 1 || endPage > totalPages)
        {
            Console.Error.WriteLine($"Invalid end page: {endPage}. Document has {totalPages} pages.");
            return;
        }

        // Use PdfFileEditor (does NOT implement IDisposable) to extract the page range
        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            // Extract pages 1 through endPage into a new PDF file
            // Correct overload: Extract(string sourceFile, int startPage, int endPage, string outputFile)
            editor.Extract(inputPath, 1, endPage, outputPath);
            Console.WriteLine($"Pages 1‑{endPage} saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}
