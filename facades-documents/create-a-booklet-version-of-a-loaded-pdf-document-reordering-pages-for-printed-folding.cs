using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes such as PdfFileEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "booklet_output.pdf"; // booklet PDF

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Create booklet version. This reorders pages for folding/printing.
            bool success = editor.MakeBooklet(inputPath, outputPath);

            if (success)
                Console.WriteLine($"Booklet created successfully: '{outputPath}'");
            else
                Console.Error.WriteLine("Booklet creation failed.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., I/O, Aspose.Pdf exceptions)
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}