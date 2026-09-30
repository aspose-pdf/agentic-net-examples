using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string sourcePath = "input.pdf";
        const string resultPath = "output.pdf";

        // Pages to delete (1‑based indexing as required by Aspose.Pdf.Facades)
        int[] pagesToDelete = new int[] { 2, 4 };

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create the PdfFileEditor instance (it does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Correct overload: Delete(string inputFile, int[] pages, string outputFile)
        editor.Delete(sourcePath, pagesToDelete, resultPath);

        Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} deleted. Result saved to '{resultPath}'.");
    }
}
