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

        // Create resize parameters:
        // Top margin = 5% of page height, Bottom margin = 15% of page height.
        // Left, right margins and content size are set to auto (null).
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            leftMargin: null,
            contentsWidth: null,
            rightMargin: null,
            topMargin: PdfFileEditor.ContentsResizeValue.Percents(5),
            contentsHeight: null,
            bottomMargin: PdfFileEditor.ContentsResizeValue.Percents(15)
        );

        // Process all pages (null array means all pages)
        int[] pages = null;

        PdfFileEditor fileEditor = new PdfFileEditor();
        bool success = fileEditor.ResizeContents(inputPath, outputPath, pages, parameters);

        if (success)
            Console.WriteLine($"Resizing completed. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Resizing failed.");
    }
}