using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Pages to delete (1‑based indexing, e.g., delete pages 2 and 4)
        int[] pagesToDelete = new int[] { 2, 4 };

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open streams with proper disposal
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages and write the result to the output stream
            bool success = editor.Delete(inputStream, pagesToDelete, outputStream);

            if (success)
                Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages.");
        }
    }
}