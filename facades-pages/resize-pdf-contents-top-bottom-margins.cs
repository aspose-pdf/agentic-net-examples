using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PDF file editor
        PdfFileEditor editor = new PdfFileEditor();

        // Configure resize parameters:
        // - Top margin = 10% of page height
        // - Bottom margin = 20% of page height
        // - Left and right margins are left as auto (centered)
        PdfFileEditor.ContentsResizeParameters resizeParams = new PdfFileEditor.ContentsResizeParameters();
        resizeParams.TopMargin = PdfFileEditor.ContentsResizeValue.Percents(10);
        resizeParams.BottomMargin = PdfFileEditor.ContentsResizeValue.Percents(20);

        // Apply the resize to all pages (pages = null)
        bool success = editor.ResizeContents(inputPath, outputPath, null, resizeParams);

        if (success)
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Resize operation failed.");
    }
}