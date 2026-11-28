using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "three_step_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using.
            PdfFileEditor editor = new PdfFileEditor();

            // Pages are 1‑based in Aspose.Pdf.
            int[] pagesToExtract = new int[] { 9, 18, 27 };

            // Correct argument order: source file, pages array, destination file.
            editor.Extract(inputPath, pagesToExtract, outputPath);

            Console.WriteLine($"Pages 9, 18, 27 extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
