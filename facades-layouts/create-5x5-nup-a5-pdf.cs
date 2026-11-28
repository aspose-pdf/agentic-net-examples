using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_5x5_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable; instantiate directly.
            PdfFileEditor editor = new PdfFileEditor();

            // MakeNUp expects an array of source PDFs. For a single source PDF we pass an array with one element.
            // The third argument indicates whether the resulting pages should be landscape.
            // Since we want a 5×5 N‑up layout on an A5 page, we keep the orientation portrait (isLandscape = false).
            string[] sourceFiles = new string[] { inputPath };
            bool isLandscape = false;

            bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);
            if (!success)
            {
                Console.Error.WriteLine("MakeNUp operation failed.");
                return;
            }

            Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
