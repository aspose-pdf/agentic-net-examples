using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use the MakeBooklet overload that does not require a PageSize argument.
        // It returns a bool indicating success.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(inputPath, outputPath);
        if (!success)
        {
            Console.Error.WriteLine("Failed to create booklet.");
            return;
        }

        Console.WriteLine($"Booklet created successfully at '{outputPath}'.");
    }
}