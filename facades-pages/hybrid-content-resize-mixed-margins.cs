using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create hybrid resize parameters:
        // - Left margin: 10% of page width (percentage)
        // - Right margin: 50 units (absolute)
        // - Top margin: 5% of page height (percentage)
        // - Bottom margin: 30 units (absolute)
        // Contents width and height are left null for automatic calculation.
        PdfFileEditor.ContentsResizeParameters resizeParams = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10), // left margin (percent)
            null,                                          // contents width (auto)
            PdfFileEditor.ContentsResizeValue.Units(50),   // right margin (units)
            PdfFileEditor.ContentsResizeValue.Percents(5), // top margin (percent)
            null,                                          // contents height (auto)
            PdfFileEditor.ContentsResizeValue.Units(30)    // bottom margin (units)
        );

        // Example usage: resize pages 1 and 2 of a PDF using the hybrid parameters.
        PdfFileEditor editor = new PdfFileEditor();
        const string sourcePath = "input.pdf";
        const string destinationPath = "output.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Resize contents with the specified hybrid margins.
        editor.ResizeContents(sourcePath, destinationPath, new int[] { 1, 2 }, resizeParams);
        Console.WriteLine("Resize operation completed successfully.");
    }
}