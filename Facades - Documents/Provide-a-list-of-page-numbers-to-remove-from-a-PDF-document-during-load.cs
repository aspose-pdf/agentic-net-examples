using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // List of page numbers to remove (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 4, 5 };

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so a plain instance is sufficient
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and write the result to a new file
        bool success = editor.Delete(inputPdf, pagesToDelete, outputPdf);

        // Report the outcome
        Console.WriteLine(success
            ? $"Deleted pages {string.Join(", ", pagesToDelete)}. Output saved to '{outputPdf}'."
            : "Failed to delete pages.");
    }
}