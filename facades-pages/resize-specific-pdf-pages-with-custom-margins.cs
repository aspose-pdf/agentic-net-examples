using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileEditor facade
        PdfFileEditor editor = new PdfFileEditor();

        // Pages to resize (2 and 4) – 1‑based indexing
        int[] pagesToResize = new int[] { 2, 4 };

        // Create resize parameters with custom left and right margins.
        // Left margin = 20 units, Right margin = 30 units.
        // Top and bottom margins are set to 0 (auto calculation).
        PdfFileEditor.ContentsResizeParameters resizeParams =
            PdfFileEditor.ContentsResizeParameters.Margins(20, 30, 0, 0);

        // Perform the resize operation
        bool result = editor.ResizeContents(inputPath, outputPath, pagesToResize, resizeParams);

        if (!result)
        {
            Console.Error.WriteLine("Failed to resize the specified pages.");
        }
        else
        {
            Console.WriteLine($"Pages 2 and 4 resized successfully. Output saved to '{outputPath}'.");
        }
    }
}