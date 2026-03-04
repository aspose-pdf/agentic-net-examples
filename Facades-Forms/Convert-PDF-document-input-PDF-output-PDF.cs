using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use the PdfFileEditor facade to copy the PDF.
        // Concatenate can merge multiple PDFs; passing a single file
        // effectively creates an identical copy.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(new string[] { inputPath }, outputPath);

        Console.WriteLine($"PDF successfully copied to '{outputPath}'.");
    }
}