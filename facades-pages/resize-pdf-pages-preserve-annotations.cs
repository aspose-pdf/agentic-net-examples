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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade (does NOT implement IDisposable)
        Aspose.Pdf.Facades.PdfFileEditor fileEditor = new Aspose.Pdf.Facades.PdfFileEditor();

        // Define new page size (width and height in default PDF units – points).
        // Example: A4 size – 595 x 842 points.
        double newPageWidth  = 595; // points
        double newPageHeight = 842; // points

        // Create resize parameters that change the page size.
        // This keeps existing content (including annotations) and adds blank margins if needed.
        Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeParameters resizeParams =
            Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeParameters.PageResize(newPageWidth, newPageHeight);

        // Apply the resize to all pages (pages = null) and save the result.
        // The ResizeContents method preserves annotations and other interactive elements.
        bool success = fileEditor.ResizeContents(
            source:      inputPath,
            destination: outputPath,
            pages:       null,          // null = all pages
            parameters:  resizeParams);

        if (success)
        {
            Console.WriteLine($"PDF resized successfully and saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize PDF.");
        }

        // No need to dispose PdfFileEditor (it does not implement IDisposable).
    }
}