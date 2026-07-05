using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_shrink.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor provides facade methods for page content resizing.
        PdfFileEditor editor = new PdfFileEditor();

        // Resize contents to 90% of original size (5% margin on each side).
        // Passing null for pages processes all pages.
        bool resized = editor.ResizeContentsPct(inputPath, outputPath, null, 90, 90);

        if (resized)
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to resize PDF contents.");
    }
}