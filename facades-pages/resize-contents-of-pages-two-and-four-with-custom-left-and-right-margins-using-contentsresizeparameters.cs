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

        // Create resize parameters with custom left and right margins.
        // Top and bottom margins are set to 0 (auto‑calculated content size).
        var parameters = PdfFileEditor.ContentsResizeParameters.Margins(
            left: 20,    // left margin in default space units
            right: 30,   // right margin in default space units
            top: 0,
            bottom: 0);

        // Pages are 1‑based; resize pages 2 and 4.
        int[] pagesToResize = new int[] { 2, 4 };

        // Use PdfFileEditor to resize the specified pages and save the result.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.ResizeContents(inputPath, outputPath, pagesToResize, parameters);

        if (!success)
        {
            Console.Error.WriteLine("Resize operation failed.");
        }
        else
        {
            Console.WriteLine($"Pages resized and saved to '{outputPath}'.");
        }
    }
}