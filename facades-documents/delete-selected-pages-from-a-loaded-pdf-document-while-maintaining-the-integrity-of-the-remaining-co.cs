using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Page numbers to delete (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and write the result to a new file
        bool succeeded = editor.Delete(inputPath, pagesToDelete, outputPath);

        // Report the outcome
        if (succeeded)
            Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} deleted successfully. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to delete pages from the PDF.");
    }
}