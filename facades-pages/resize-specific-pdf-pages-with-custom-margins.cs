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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Pages are 1‑based; resize pages 2 and 4
        int[] pagesToResize = new int[] { 2, 4 };

        // Define custom left and right margins (in default space units).
        // Top and bottom margins are set to 0 (no change).
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(
            left: 20,   // left margin
            right: 30,  // right margin
            top: 0,     // top margin
            bottom: 0   // bottom margin
        );

        // Perform the resize operation using the Facades API.
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.ResizeContents(inputPath, outputPath, pagesToResize, resizeParams);

        if (result)
        {
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize PDF pages.");
        }
    }
}