using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "nup_2x2.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
            var editor = new PdfFileEditor();

            // Create a 2‑by‑2 N‑up layout (2 rows, 2 columns)
            // Overload: MakeNUp(string sourceFile, string destinationFile, int nUpRows, int nUpColumns)
            // Use positional arguments because the parameter names in the library are not 'rows'/'columns'.
            editor.MakeNUp(inputPath, outputPath, 2, 2);

            Console.WriteLine($"N‑up PDF created: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during N‑up creation: {ex.Message}");
        }
    }
}
