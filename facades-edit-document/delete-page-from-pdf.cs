using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so no using block is required
        PdfFileEditor editor = new PdfFileEditor();

        // Delete page 5 (Aspose.Pdf uses 1‑based page numbers)
        bool result = editor.Delete(inputPath, new int[] { 5 }, outputPath);

        if (result)
        {
            Console.WriteLine($"Page 5 removed successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete the specified page.");
        }
    }
}