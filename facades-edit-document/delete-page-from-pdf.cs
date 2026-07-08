using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing at least five pages
        const string inputPath  = "input.pdf";
        // Output PDF after page 5 has been removed
        const string outputPath = "output.pdf";

        // Verify the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor provides high‑level operations on PDF files.
        // The Delete method removes pages specified by a 1‑based page number array
        // and writes the result directly to the output file.
        PdfFileEditor editor = new PdfFileEditor();

        // Delete page 5
        bool deleted = editor.Delete(inputPath, new int[] { 5 }, outputPath);

        if (deleted)
        {
            Console.WriteLine($"Page 5 successfully deleted. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete the specified page.");
        }
    }
}