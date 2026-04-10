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

        // Shrink contents to 95% of original size (5% negative margin) on all pages
        double newWidthPct = 95.0;
        double newHeightPct = 95.0;

        PdfFileEditor editor = new PdfFileEditor();
        bool resized = editor.ResizeContentsPct(inputPath, outputPath, null, newWidthPct, newHeightPct);

        if (resized)
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to resize PDF contents.");
    }
}