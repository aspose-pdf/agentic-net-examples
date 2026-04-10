using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // Define resize parameters:
        // - Top margin = 20% of page height
        // - Bottom margin = 10% of page height
        // - Left, right margins and content size are calculated automatically (null)
        PdfFileEditor.ContentsResizeParameters parameters =
            new PdfFileEditor.ContentsResizeParameters(
                leftMargin:      null,                                            // auto
                contentsWidth:   null,                                            // auto
                rightMargin:     null,                                            // auto
                topMargin:       PdfFileEditor.ContentsResizeValue.Percents(20), // 20% top
                contentsHeight:  null,                                            // auto
                bottomMargin:    PdfFileEditor.ContentsResizeValue.Percents(10)  // 10% bottom
            );

        // Resize contents of all pages (pages = null) using the defined parameters
        bool success = editor.ResizeContents(
            source:      inputPath,
            destination: outputPath,
            pages:       null,      // null = all pages
            parameters:  parameters
        );

        if (success)
            Console.WriteLine($"Pages resized successfully. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Resize operation failed.");
    }
}
