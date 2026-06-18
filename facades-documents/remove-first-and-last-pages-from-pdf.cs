using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine total number of pages using Document (wrapped in using for proper disposal)
        int totalPages;
        using (Document doc = new Document(inputPath))
        {
            totalPages = doc.Pages.Count;
        }

        // Prepare array with first and last page numbers (1‑based indexing)
        int[] pagesToDelete = new int[] { 1, totalPages };

        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and save the result
        bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

        if (success)
            Console.WriteLine($"First and last pages removed. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to delete pages.");
    }
}