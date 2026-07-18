using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output_hybrid.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Create hybrid resize parameters:
        //   - Left margin  : 10% of the page width (percentage)
        //   - Right margin : 20 units (absolute)
        //   - Top margin   : 5% of the page height (percentage)
        //   - Bottom margin: 30 units (absolute)
        //   - Contents width/height are left null for automatic calculation
        // -----------------------------------------------------------------
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10), // left margin (percent)
            null,                                          // contents width (auto)
            PdfFileEditor.ContentsResizeValue.Units(20),   // right margin (units)
            PdfFileEditor.ContentsResizeValue.Percents(5), // top margin (percent)
            null,                                          // contents height (auto)
            PdfFileEditor.ContentsResizeValue.Units(30)    // bottom margin (units)
        );

        // Initialize the PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();

        // Resize contents of all pages using the hybrid parameters
        // Passing null for the pages array processes every page in the document
        bool success = editor.ResizeContents(inputPath, outputPath, null, parameters);

        // Report the result
        Console.WriteLine(success
            ? $"Hybrid resize completed successfully. Output saved to '{outputPath}'."
            : "Hybrid resize failed.");
    }
}