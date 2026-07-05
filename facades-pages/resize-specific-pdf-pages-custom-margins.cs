using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Instantiate the facade that provides page‑editing operations
        PdfFileEditor editor = new PdfFileEditor();

        // Pages are 1‑based; we want to resize pages 2 and 4
        int[] pagesToResize = new int[] { 2, 4 };

        // Create resize parameters:
        // - Left margin = 10 % of page width
        // - Right margin = 10 % of page width
        // - All other values (contents size, top/bottom margins) are left null
        //   so they are calculated automatically.
        PdfFileEditor.ContentsResizeParameters parameters =
            new PdfFileEditor.ContentsResizeParameters(
                PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
                null,                                          // contents width (auto)
                PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
                null,                                          // top margin (auto)
                null,                                          // contents height (auto)
                null                                           // bottom margin (auto)
            );

        // Perform the resize operation; the method loads the source PDF,
        // applies the margins to the specified pages, and saves the result.
        bool result = editor.ResizeContents(inputPath, outputPath, pagesToResize, parameters);

        if (result)
            Console.WriteLine($"Pages 2 and 4 resized successfully. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to resize the specified pages.");
    }
}