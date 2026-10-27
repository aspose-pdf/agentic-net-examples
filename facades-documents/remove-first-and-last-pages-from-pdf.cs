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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the first and last page numbers using a Document (must be disposed)
        int firstPage;
        int lastPage;
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            firstPage = 1;
            lastPage  = doc.Pages.Count;
        }

        // Prepare the array of pages to delete (first and last)
        int[] pagesToDelete = new int[] { firstPage, lastPage };

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and write the result to outputPath
        // Correct parameter order: input file, pages to delete, output file
        editor.Delete(inputPath, pagesToDelete, outputPath);

        Console.WriteLine($"First and last pages removed. Result saved to '{outputPath}'.");
    }
}
