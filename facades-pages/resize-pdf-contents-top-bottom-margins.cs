using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create resize parameters:
        // - Left and right margins: 5% of page width each
        // - Top margin: 15% of page height
        // - Bottom margin: 5% of page height
        // - Contents width and height are set to Auto so they are calculated
        //   from the remaining space after margins are applied.
        var resizeParams = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(5),   // left margin
            PdfFileEditor.ContentsResizeValue.Auto(),       // contents width (auto)
            PdfFileEditor.ContentsResizeValue.Percents(5),   // right margin
            PdfFileEditor.ContentsResizeValue.Percents(15),  // top margin
            PdfFileEditor.ContentsResizeValue.Auto(),       // contents height (auto)
            PdfFileEditor.ContentsResizeValue.Percents(5)    // bottom margin
        );

        // Instantiate PdfFileEditor (do NOT use a using block; it is not IDisposable)
        var editor = new PdfFileEditor();

        // Resize contents of all pages (pages = null) using the defined parameters
        bool success = editor.ResizeContents(inputPath, outputPath, null, resizeParams);

        // Report the result
        if (success)
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to resize PDF contents.");
    }
}