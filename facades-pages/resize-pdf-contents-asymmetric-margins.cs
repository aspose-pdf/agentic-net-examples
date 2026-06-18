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

        // Create an instance of PdfFileEditor (no IDisposable, so no using block)
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Define asymmetric margins:
        //   Left  = 10% of page width
        //   Right = 20% of page width
        //   Top   = 5%  of page height
        //   Bottom= 5%  of page height
        // Contents width/height are set to Auto (null) so they are calculated automatically.
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10),   // left margin
            null,                                            // auto contents width
            PdfFileEditor.ContentsResizeValue.Percents(20),   // right margin
            PdfFileEditor.ContentsResizeValue.Percents(5),    // top margin
            null,                                            // auto contents height
            PdfFileEditor.ContentsResizeValue.Percents(5)    // bottom margin
        );

        // Resize pages 1, 2 and 3; pass null to process all pages if needed
        int[] pagesToResize = new int[] { 1, 2, 3 };

        // Perform the resize operation
        bool success = fileEditor.ResizeContents(
            inputPath,
            outputPath,
            pagesToResize,
            parameters
        );

        // Report the result
        if (success)
            Console.WriteLine($"Contents resized successfully. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Resize operation failed.");
    }
}