using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPath = "destination.pdf";
        const string sourcePath = "source.pdf";

        // Verify that both files exist
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load source document only to obtain the page count
            Document srcDoc = new Document(sourcePath);
            int pageCount = srcDoc.Pages.Count;
            int[] sourcePages = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
                sourcePages[i] = i + 1; // Aspose.Pdf uses 1‑based page numbers

            // Use PdfFileEditor to insert all source pages at the end of the destination PDF
            PdfFileEditor editor = new PdfFileEditor();
            // int.MaxValue forces insertion after the last existing page
            editor.Insert(destinationPath, int.MaxValue, sourcePath, sourcePages, destinationPath);

            Console.WriteLine($"Successfully appended {pageCount} page(s) from '{sourcePath}' to '{destinationPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during append operation: {ex.Message}");
        }
    }
}
