using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired end page (inclusive) for the split operation
        const int endPage = 5;
        // Output PDF file path (front part from page 1 to endPage)
        const string outputPath = "output_split.pdf";

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is required
            PdfFileEditor editor = new PdfFileEditor();

            // SplitFromFirst splits the PDF from the first page up to the specified location
            // and saves the front part as a new file.
            bool success = editor.SplitFromFirst(inputPath, endPage, outputPath);

            if (success)
                Console.WriteLine($"PDF successfully split. Front part saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Split operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}