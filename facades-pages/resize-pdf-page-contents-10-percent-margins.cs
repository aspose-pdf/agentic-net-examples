using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the facade for PDF file editing
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Define resize parameters: 10% margins on all sides
        PdfFileEditor.ContentsResizeParameters parameters =
            PdfFileEditor.ContentsResizeParameters.MarginsPercent(10, 10, 10, 10);

        // Resize contents of all pages (pages = null means all pages)
        bool success = fileEditor.ResizeContents(inputPath, outputPath, null, parameters);

        if (success)
            Console.WriteLine($"Pages resized successfully. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to resize pages.");
    }
}