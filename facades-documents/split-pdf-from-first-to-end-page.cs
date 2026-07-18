using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF will contain pages from the first page up to endPage (inclusive)
        const string outputPath = "split_output.pdf";
        // Specify the last page to include in the split
        int endPage = 5;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor provides file‑level operations such as splitting
            PdfFileEditor editor = new PdfFileEditor();

            // SplitFromFirst extracts pages 1..endPage and saves them to outputPath
            bool success = editor.SplitFromFirst(inputPath, endPage, outputPath);

            Console.WriteLine(success
                ? $"Successfully created '{outputPath}' containing pages 1‑{endPage}."
                : "Split operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}