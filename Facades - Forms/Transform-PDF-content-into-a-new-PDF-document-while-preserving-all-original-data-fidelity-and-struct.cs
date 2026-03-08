using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor is a facade that works directly on PDF files.
        // Concatenate with a single source file creates an exact copy,
        // preserving all original data, structure, annotations, tags, etc.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(new[] { inputPath }, outputPath);

        Console.WriteLine($"Transformed PDF saved to '{outputPath}'.");
    }
}