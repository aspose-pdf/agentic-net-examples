using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open streams with deterministic disposal
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages; the method returns void, so just invoke it
            editor.Delete(inputStream, pagesToDelete, outputStream);
        }

        Console.WriteLine($"Deleted pages {string.Join(", ", pagesToDelete)}. Output saved to '{outputPath}'.");
    }
}
