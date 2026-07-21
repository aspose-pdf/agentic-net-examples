using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Pages to delete (1‑based indexing, e.g., delete pages 2 and 3)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open input and output streams with proper disposal
            using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block
                PdfFileEditor editor = new PdfFileEditor();

                // Delete the specified pages; the method returns true on success
                bool success = editor.Delete(inputStream, pagesToDelete, outputStream);

                if (success)
                {
                    Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Failed to delete pages from the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}