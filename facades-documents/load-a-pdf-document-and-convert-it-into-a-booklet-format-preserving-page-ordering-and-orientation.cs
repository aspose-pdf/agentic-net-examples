using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for PDF manipulation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is required
            PdfFileEditor editor = new PdfFileEditor();

            // MakeBooklet creates a booklet version of the PDF.
            // This method preserves page ordering and orientation automatically.
            bool success = editor.MakeBooklet(inputPath, outputPath);

            if (success)
                Console.WriteLine($"Booklet created successfully: '{outputPath}'");
            else
                Console.Error.WriteLine("Booklet creation failed.");
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Pdf or IO exceptions and report them
            Console.Error.WriteLine($"Error during booklet conversion: {ex.Message}");
        }
    }
}