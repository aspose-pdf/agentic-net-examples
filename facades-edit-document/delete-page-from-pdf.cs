using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        // The Delete method removes the specified pages (1‑based indexing) and writes the result.
        PdfFileEditor editor = new PdfFileEditor();

        // Delete page 5 from the document.
        bool deleted = editor.Delete(inputPath, new int[] { 5 }, outputPath);

        if (deleted)
        {
            Console.WriteLine($"Page 5 successfully removed. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete the specified page.");
        }
    }
}