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
        int[] pagesToDelete = new int[] { 2, 3 }; // pages are 1‑based

        // Verify that the source file exists before invoking the facade
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPath}'.");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages and write the result to a new file
            editor.Delete(inputPath, pagesToDelete, outputPath);

            Console.WriteLine($"Successfully deleted pages and saved to '{outputPath}'.");
        }
        catch (PdfException ex)
        {
            // Thrown when the input file is not a valid PDF or other PDF‑specific errors occur
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch‑all for any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
