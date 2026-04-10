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

        // Determine total number of pages in the source PDF
        int totalPages;
        using (Document doc = new Document(inputPath))
        {
            totalPages = doc.Pages.Count;
        }

        // Prepare array with first and last page numbers (1‑based indexing)
        int[] pagesToDelete = new int[] { 1, totalPages };

        // Use PdfFileEditor to delete the specified pages
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Delete(inputPath, pagesToDelete, outputPath);

        if (result)
            Console.WriteLine($"First and last pages removed. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to delete pages.");
    }
}