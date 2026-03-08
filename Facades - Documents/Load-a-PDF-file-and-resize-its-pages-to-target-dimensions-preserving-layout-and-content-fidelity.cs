using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        // Target page dimensions in points (1 point = 1/72 inch)
        // Example: A5 size (420 x 595 points)
        const double targetWidth  = 420.0;
        const double targetHeight = 595.0;

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileEditor instance (no IDisposable implementation required)
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Build resize parameters that set the new page size.
        // PageResize creates parameters for changing the page dimensions while preserving content.
        PdfFileEditor.ContentsResizeParameters resizeParams =
            PdfFileEditor.ContentsResizeParameters.PageResize(targetWidth, targetHeight);

        // Resize all pages (pages array = null) and write the result to the output file.
        // This method preserves the original layout and content fidelity.
        bool success = fileEditor.ResizeContents(
            source:      inputPath,
            destination: outputPath,
            pages:       null,          // null = all pages
            parameters:  resizeParams);

        if (success)
        {
            Console.WriteLine($"PDF pages resized successfully and saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize PDF pages.");
        }
    }
}