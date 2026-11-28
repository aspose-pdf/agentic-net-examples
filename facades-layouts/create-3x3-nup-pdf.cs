using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor and MakeNUp are here

class Program
{
    static void Main()
    {
        const string inputPath  = "documentA.pdf";
        const string outputPath = "nup_3x3.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // MakeNUp creates an N‑up layout. It accepts an array of source PDFs,
            // the output path, and a flag indicating whether the resulting pages
            // should be in landscape orientation. For a 3×3 layout we set the
            // N‑up configuration via the editor's internal defaults (3 rows × 3 columns).
            // The method automatically arranges up to 9 pages per sheet.
            string[] sourceFiles = new[] { inputPath };
            bool isLandscape = false; // keep portrait orientation; change to true for landscape

            bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);
            if (success)
                Console.WriteLine($"N‑up PDF created successfully: {outputPath}");
            else
                Console.Error.WriteLine("N‑up operation failed without throwing an exception.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during N‑up processing: {ex.Message}");
        }
    }
}
