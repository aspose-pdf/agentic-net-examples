using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_hybrid.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create hybrid resize parameters with mixed percentage and absolute margins
        // - Left margin: 10% of page width
        // - Right margin: 20 points (absolute)
        // - Top margin: 5% of page height
        // - Bottom margin: 30 points (absolute)
        // Width and height are left null (auto) – they are not set in the parameters object.
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters
        {
            LeftMargin   = PdfFileEditor.ContentsResizeValue.Percents(10),   // 10% left
            RightMargin  = PdfFileEditor.ContentsResizeValue.Units(20),      // 20 points right
            TopMargin    = PdfFileEditor.ContentsResizeValue.Percents(5),    // 5% top
            BottomMargin = PdfFileEditor.ContentsResizeValue.Units(30)       // 30 points bottom
            // Width and Height remain null (auto)
        };

        // Initialize the PdfFileEditor and apply the resize to all pages (pages = null)
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.ResizeContents(inputPath, outputPath, null, parameters);

        // Report the result
        Console.WriteLine(success
            ? $"Hybrid resize completed successfully. Output saved to '{outputPath}'."
            : "Hybrid resize operation failed.");
    }
}
