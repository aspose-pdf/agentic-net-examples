using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_stripped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Instantiate the JavaScript stripper and remove all scripts from the PDF.
        PdfJavaScriptStripper stripper = new PdfJavaScriptStripper();
        bool result = stripper.Strip(inputPath, outputPath);

        Console.WriteLine(result
            ? $"All JavaScript actions removed. Output saved to '{outputPath}'."
            : "Failed to strip JavaScript from the document.");
    }
}