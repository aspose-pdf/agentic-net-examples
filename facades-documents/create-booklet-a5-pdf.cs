using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the facade for PDF file operations
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet with custom A5 page size
            bool result = editor.MakeBooklet(inputPath, outputPath, PageSize.A5);

            Console.WriteLine(result
                ? $"Booklet created successfully at '{outputPath}'."
                : "Failed to create booklet.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}