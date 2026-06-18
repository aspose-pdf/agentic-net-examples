using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Delete page number 5 (pages are 1‑based) and save the result
            bool deleted = editor.Delete(inputPath, new int[] { 5 }, outputPath);

            if (deleted)
                Console.WriteLine($"Page 5 removed successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete the specified page.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during deletion: {ex.Message}");
        }
    }
}