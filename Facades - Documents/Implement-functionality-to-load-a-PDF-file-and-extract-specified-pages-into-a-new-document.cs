using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path for the extracted pages
        const string outputPath = "extracted_pages.pdf";

        // Define the page range to extract (1‑based indexing)
        const int startPage = 2;
        const int endPage   = 5;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // PdfFileEditor provides the Extract method that loads the source PDF,
        // extracts the specified page range, and saves the result to a new file.
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the extraction. The method returns true on success.
        bool extracted = editor.Extract(inputPath, startPage, endPage, outputPath);

        if (extracted)
        {
            Console.WriteLine($"Successfully extracted pages {startPage}-{endPage} to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Extraction failed.");
        }
    }
}