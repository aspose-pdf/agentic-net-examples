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

        // Create the PdfFileEditor facade
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Shrink page contents uniformly by 5% (new size = 95% of original)
        // Passing null for pages processes all pages
        bool success = fileEditor.ResizeContentsPct(inputPath, outputPath, null, 95, 95);

        if (success)
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Resize operation failed.");
    }
}