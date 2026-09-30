using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "input.pdf";
        const string outputPath = "extracted_pages.pdf";
        const int endPage = 5; // extract pages from the start up to this page (inclusive)

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Use SplitFromFirst to extract pages from the beginning up to endPage.
        // Signature: SplitFromFirst(string sourceFile, int endPage, string outputFile)
        editor.SplitFromFirst(sourcePath, endPage, outputPath);

        Console.WriteLine($"Pages 1 to {endPage} have been saved to '{outputPath}'.");
    }
}
