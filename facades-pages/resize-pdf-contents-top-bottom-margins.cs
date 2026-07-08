using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string sourcePath = "input.pdf";
        const string destinationPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create a PdfFileEditor instance (no IDisposable implementation)
        PdfFileEditor editor = new PdfFileEditor();

        // Define resize parameters:
        // Left and right margins = 10% of page width,
        // Top margin = 20% of page height,
        // Bottom margin = 5% of page height.
        // Contents width/height are set to null so they are calculated automatically.
        PdfFileEditor.ContentsResizeParameters parameters =
            new PdfFileEditor.ContentsResizeParameters(
                PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
                null,                                          // contents width (auto)
                PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
                PdfFileEditor.ContentsResizeValue.Percents(20), // top margin
                null,                                          // contents height (auto)
                PdfFileEditor.ContentsResizeValue.Percents(5)   // bottom margin
            );

        // Apply the resize to all pages (pages = null)
        bool success = editor.ResizeContents(sourcePath, destinationPath, null, parameters);

        if (success)
            Console.WriteLine($"Pages resized successfully. Output saved to '{destinationPath}'.");
        else
            Console.Error.WriteLine("Failed to resize pages.");
    }
}