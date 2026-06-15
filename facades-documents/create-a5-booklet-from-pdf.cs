using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "booklet_A5.pdf";

        // Verify that the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so no using block is required
        PdfFileEditor editor = new PdfFileEditor();

        // Create a booklet with A5 page size using the overload that accepts PageSize
        bool result = editor.MakeBooklet(inputPath, outputPath, PageSize.A5);

        Console.WriteLine(result
            ? $"Booklet created successfully at '{outputPath}'."
            : "Failed to create booklet.");
    }
}