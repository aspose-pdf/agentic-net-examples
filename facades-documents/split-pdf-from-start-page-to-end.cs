using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF to split
        const string sourcePath = "input.pdf";
        // Output PDF containing pages from startPage to the end
        const string outputPath = "split_from_page5.pdf";
        // Page number to start splitting (Aspose.Pdf uses 1‑based indexing)
        const int startPage = 5;

        // Verify the source file exists before attempting the operation
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Split the PDF from startPage to the end and save to outputPath
            editor.SplitToEnd(sourcePath, startPage, outputPath);
            Console.WriteLine($"PDF successfully split. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}