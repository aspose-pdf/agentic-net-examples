using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, temporary file after deletion, and final booklet output
        const string inputPath = "input.pdf";
        const string tempPath = "temp_deleted.pdf";
        const string outputPath = "booklet.pdf";

        // Example: pages to remove (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade instance (PdfFileEditor does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // 1. Delete unwanted pages and write to a temporary file
        bool deleteSuccess = editor.Delete(inputPath, pagesToDelete, tempPath);
        if (!deleteSuccess)
        {
            Console.Error.WriteLine("Failed to delete specified pages.");
            return;
        }

        // 2. Generate a booklet from the temporary file
        bool bookletSuccess = editor.MakeBooklet(tempPath, outputPath);
        if (!bookletSuccess)
        {
            Console.Error.WriteLine("Failed to create booklet.");
            return;
        }

        // Optional: clean up the intermediate file
        try { File.Delete(tempPath); } catch { }

        Console.WriteLine($"Booklet created successfully: {outputPath}");
    }
}