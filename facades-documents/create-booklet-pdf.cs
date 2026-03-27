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

        PdfFileEditor pdfEditor = new PdfFileEditor();
        bool success = pdfEditor.MakeBooklet(inputPath, outputPath);
        if (success)
        {
            Console.WriteLine($"Booklet created: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}