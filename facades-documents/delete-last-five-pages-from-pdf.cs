using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Obtain total page count using Document (wrapped in using for proper disposal)
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // If the document has fewer than 5 pages, nothing to delete
        if (pageCount < 5)
        {
            Console.WriteLine("Document has fewer than 5 pages; no pages were deleted.");
            // Optionally copy the original file to the output location
            File.Copy(inputPath, outputPath, true);
            return;
        }

        // Determine the page numbers of the last five pages (Aspose.Pdf uses 1‑based indexing)
        int[] pagesToDelete = new int[5];
        for (int i = 0; i < 5; i++)
        {
            pagesToDelete[i] = pageCount - i;
        }

        // PdfFileEditor does NOT implement IDisposable; instantiate normally
        PdfFileEditor editor = new PdfFileEditor();
        // NOTE: In Aspose.Pdf 26.x the Delete method signature is
        // Delete(string inputPath, int[] pages, string outputPath)
        // so the page array must be the second argument.
        editor.Delete(inputPath, pagesToDelete, outputPath);

        Console.WriteLine($"Deleted pages {string.Join(", ", pagesToDelete)}. Saved result to '{outputPath}'.");
    }
}
