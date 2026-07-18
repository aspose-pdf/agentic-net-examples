using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";
        // Path where the extracted front part will be saved
        const string outputPath = "front_part.pdf";
        // The page number up to which the PDF should be split (inclusive)
        int endPage = 5;

        // Verify that the source file exists before attempting the operation
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is required
            PdfFileEditor editor = new PdfFileEditor();

            // SplitFromFirst extracts pages from the first page up to 'endPage' and saves them
            bool success = editor.SplitFromFirst(inputPath, endPage, outputPath);

            if (success)
                Console.WriteLine($"Successfully extracted pages 1-{endPage} to '{outputPath}'.");
            else
                Console.WriteLine("SplitFromFirst returned false – operation may have failed.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}