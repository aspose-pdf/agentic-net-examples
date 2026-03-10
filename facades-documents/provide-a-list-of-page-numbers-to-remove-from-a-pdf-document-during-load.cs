using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output PDF file path (after pages are removed)
        const string outputPdf = "output.pdf";

        // Page numbers to delete – 1‑based indexing as required by PdfFileEditor
        int[] pagesToRemove = new int[] { 2, 4, 7 };

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the PdfFileEditor instance (no using – it does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and save the result
        bool success = editor.Delete(inputPdf, pagesToRemove, outputPdf);

        if (success)
        {
            Console.WriteLine($"Pages {string.Join(", ", pagesToRemove)} removed successfully.");
            Console.WriteLine($"Result saved to: {outputPdf}");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete pages.");
        }
    }
}