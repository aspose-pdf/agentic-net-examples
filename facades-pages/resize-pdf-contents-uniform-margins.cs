using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor instance (no IDisposable required)
        PdfFileEditor editor = new PdfFileEditor();

        // Define uniform margins of 10 % on each side.
        // ContentsResizeValue.Percents creates a margin expressed as a percentage of the original page size.
        // Passing null for content width/height lets the API calculate them automatically.
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
            null,                                          // auto‑calculate content width
            PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
            PdfFileEditor.ContentsResizeValue.Percents(10), // top margin
            null,                                          // auto‑calculate content height
            PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin
        );

        // Resize all pages (pages array = null) using the defined parameters.
        bool success = editor.ResizeContents(inputPath, outputPath, null, parameters);

        Console.WriteLine(success ? "Page contents resized with uniform margins." : "Resize operation failed.");
    }
}