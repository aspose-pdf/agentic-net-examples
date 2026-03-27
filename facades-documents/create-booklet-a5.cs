using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(inputPath, outputPath, PageSize.A5);

        if (success)
        {
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}