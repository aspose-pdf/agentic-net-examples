using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Aspose.Pdf.Facades provides PdfFileEditor for file‑level operations.
            // Concatenating a single file creates an exact copy of the source PDF.
            PdfFileEditor editor = new PdfFileEditor();
            editor.Concatenate(new string[] { inputPath }, outputPath);

            Console.WriteLine($"PDF successfully converted (copied) to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during the operation.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}