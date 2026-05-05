using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "temp_inserted.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get the total number of pages (1‑based indexing)
        int lastPage;
        using (Document doc = new Document(inputPath))
        {
            lastPage = doc.Pages.Count;
        }

        // Insert the last page at the beginning, creating an intermediate file
        PdfFileEditor editor = new PdfFileEditor();
        editor.Insert(inputPath, 1, inputPath, lastPage, lastPage, tempPath);

        // Delete the original last page (now positioned after the inserted page)
        int[] pagesToDelete = new int[] { lastPage + 1 };
        editor.Delete(tempPath, pagesToDelete, outputPath);

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}