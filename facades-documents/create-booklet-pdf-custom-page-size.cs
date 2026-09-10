using System;
using System.IO;
using Aspose.Pdf;               // PageSize enum
using Aspose.Pdf.Facades;      // PdfFileEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a booklet with a custom page size (A5)
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputPath, outputPath, PageSize.A5);

        Console.WriteLine(result ? "Booklet created successfully." : "Failed to create booklet.");
    }
}