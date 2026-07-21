using System;
using System.IO;
using Aspose.Pdf;               // Document class for page count
using Aspose.Pdf.Facades;      // PdfFileEditor for Delete operation

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

        // Determine the total number of pages using Document (must be disposed)
        int totalPages;
        using (Document doc = new Document(inputPath))
        {
            totalPages = doc.Pages.Count;
        }

        // If the PDF has fewer than two pages, just copy it to the output
        if (totalPages < 2)
        {
            File.Copy(inputPath, outputPath, overwrite: true);
            Console.WriteLine("PDF has less than two pages; copied without changes.");
            return;
        }

        // Pages to delete: first page (1) and last page (totalPages)
        int[] pagesToDelete = new int[] { 1, totalPages };

        // PdfFileEditor does NOT implement IDisposable, so no using block
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and save the result
        bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

        if (success)
            Console.WriteLine($"First and last pages removed. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to delete pages.");
    }
}