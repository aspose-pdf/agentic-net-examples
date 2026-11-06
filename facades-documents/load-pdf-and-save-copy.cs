using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string destinationPath = "destination.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Load the source PDF by extracting all its pages to a new file.
        // This demonstrates loading the PDF via PdfFileEditor.
        int[] allPages = GetAllPageNumbers(sourcePath);
        // Correct argument order: source file, page numbers, destination file.
        editor.Extract(sourcePath, allPages, destinationPath);

        Console.WriteLine($"Destination PDF created at '{destinationPath}'.");
    }

    // Helper method to obtain an array of all page numbers (1‑based) using Document.
    static int[] GetAllPageNumbers(string pdfPath)
    {
        // Document implements IDisposable, so wrap it in a using block.
        using (Document doc = new Document(pdfPath))
        {
            int pageCount = doc.Pages.Count; // Pages are 1‑based.
            int[] pages = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
                pages[i] = i + 1;
            return pages;
        }
    }
}
