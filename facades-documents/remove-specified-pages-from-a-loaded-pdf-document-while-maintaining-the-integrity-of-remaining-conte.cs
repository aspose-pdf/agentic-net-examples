using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Output PDF file path (result after pages are removed)
        const string outputPath = "output.pdf";

        // Pages to delete – page numbers are 1‑based as per Aspose.Pdf conventions
        int[] pagesToDelete = new int[] { 2, 4, 5 };

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is required.
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages and write the result to the output file.
            // The Delete method returns true on success.
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

            if (success)
                Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} removed successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}