using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_2up.pdf";

        // Verify the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileEditor to create a 2‑up layout (2 columns, 1 row)
        // The MakeNUp method expects an array of source files and a landscape flag.
        PdfFileEditor editor = new PdfFileEditor();
        editor.MakeNUp(new[] { inputPath }, outputPath, false);

        Console.WriteLine($"2‑up PDF created successfully: {outputPath}");
    }
}
