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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileEditor fileEditor = new PdfFileEditor();
        // Shrink contents to 95% of original size (5% margin) on all pages
        bool success = fileEditor.ResizeContentsPct(inputPath, outputPath, null, 95, 95);
        if (success)
        {
            Console.WriteLine($"Contents resized and saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Resize operation failed.");
        }
    }
}