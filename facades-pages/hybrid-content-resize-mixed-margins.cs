using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create hybrid resize parameters:
        // - Left margin: 10% of page width (percentage)
        // - Right margin: 20 units (absolute)
        // - Top margin: 5% of page height (percentage)
        // - Bottom margin: 15 units (absolute)
        // - Contents width and height are left as auto (null) so they are calculated automatically.
        var parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10),   // left margin (percent)
            null,                                            // contents width (auto)
            PdfFileEditor.ContentsResizeValue.Units(20),    // right margin (absolute units)
            PdfFileEditor.ContentsResizeValue.Percents(5),  // top margin (percent)
            null,                                            // contents height (auto)
            PdfFileEditor.ContentsResizeValue.Units(15)     // bottom margin (absolute units)
        );

        // Use PdfFileEditor to apply the resize to all pages (null page array means all pages)
        var editor = new PdfFileEditor();
        bool success = editor.ResizeContents(inputPath, outputPath, null, parameters);

        Console.WriteLine(success
            ? $"Resize completed successfully. Output saved to '{outputPath}'."
            : "Resize operation failed.");
    }
}