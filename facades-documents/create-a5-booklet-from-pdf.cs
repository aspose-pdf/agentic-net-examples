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

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is required.
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet with A5 page size using the appropriate overload.
            bool result = editor.MakeBooklet(inputPath, outputPath, PageSize.A5);

            if (result)
                Console.WriteLine($"Booklet created successfully: {outputPath}");
            else
                Console.Error.WriteLine("Failed to create booklet.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}