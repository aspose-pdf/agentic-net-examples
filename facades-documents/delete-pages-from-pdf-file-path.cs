using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path after deleting pages 2 through 5
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create an instance of PdfFileEditor (Facades API)
            PdfFileEditor editor = new PdfFileEditor();

            // Define the pages to delete (pages are 1‑based)
            int[] pagesToDelete = new int[] { 2, 3, 4, 5 };

            // Perform the deletion and save the result to the output file
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

            if (success)
                Console.WriteLine($"Pages 2‑5 deleted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}