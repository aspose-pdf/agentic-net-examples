using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the source PDF exists before attempting any operation.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file '{inputPath}' not found.");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        var editor = new PdfFileEditor();
        try
        {
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);
            if (success)
            {
                Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Failed to delete pages. Operation returned false.");
            }
        }
        catch (FileNotFoundException ex)
        {
            // Handles the case where Delete internally cannot locate the file.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Generic fallback for any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
