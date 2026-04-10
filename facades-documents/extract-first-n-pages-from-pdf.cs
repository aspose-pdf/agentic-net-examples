using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";
        // Path where the front part (pages 1 to endPage) will be saved
        const string outputPath = "front_part.pdf";
        // The last page to include in the split (inclusive)
        const int endPage = 5;

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // SplitFromFirst extracts pages from the first page up to 'endPage' and saves them
            bool result = editor.SplitFromFirst(inputPath, endPage, outputPath);

            if (result)
                Console.WriteLine($"Successfully saved pages 1-{endPage} to '{outputPath}'.");
            else
                Console.Error.WriteLine("SplitFromFirst returned false – operation may have failed.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}