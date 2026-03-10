using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_nup.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the N‑up layout:
        //   x = number of columns (pages placed side‑by‑side horizontally)
        //   y = number of rows    (pages placed one above another vertically)
        // For example, x = 2 and y = 2 will place 4 original pages on each sheet
        // in the order: page1, page2, page3, page4 (left‑to‑right, top‑to‑bottom).
        int x = 2; // columns
        int y = 2; // rows

        // PdfFileEditor provides N‑up functionality. Using TryMakeNUp avoids throwing
        // an exception if the operation cannot be performed; it simply returns false.
        PdfFileEditor editor = new PdfFileEditor();

        // The TryMakeNUp method reads the input PDF, creates a new PDF where each
        // output page contains x*y source pages tiled in a grid, and writes the
        // result to the specified output file.
        bool result = editor.TryMakeNUp(inputPath, outputPath, x, y);

        if (result)
        {
            Console.WriteLine($"N‑up PDF created successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
        }
    }
}