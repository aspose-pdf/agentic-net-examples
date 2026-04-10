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

        // PdfFileEditor does NOT implement IDisposable, so no using block is needed
        PdfFileEditor editor = new PdfFileEditor();

        // Delete page 5 (page numbers are 1‑based)
        bool deleted = editor.Delete(inputPath, new int[] { 5 }, outputPath);

        if (deleted)
        {
            Console.WriteLine($"Page 5 removed successfully. Saved as '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete page 5.");
        }
    }
}