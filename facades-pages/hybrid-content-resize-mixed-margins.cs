using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_hybrid.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create hybrid resize parameters:
        // - Left margin: 10% of page width (percentage)
        // - Right margin: 30 units (absolute)
        // - Top margin: 5% of page height (percentage)
        // - Bottom margin: 20 units (absolute)
        // - Contents width/height are left as auto (null) so they are calculated automatically.
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10),   // left margin (percent)
            null,                                            // contents width (auto)
            PdfFileEditor.ContentsResizeValue.Units(30),    // right margin (absolute units)
            PdfFileEditor.ContentsResizeValue.Percents(5),  // top margin (percent)
            null,                                            // contents height (auto)
            PdfFileEditor.ContentsResizeValue.Units(20)     // bottom margin (absolute units)
        );

        // Perform the resize on all pages (pages = null)
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.ResizeContents(inputPath, outputPath, null, parameters);

        Console.WriteLine(success
            ? $"Resizing succeeded. Output saved to '{outputPath}'."
            : "Resizing failed.");
    }
}