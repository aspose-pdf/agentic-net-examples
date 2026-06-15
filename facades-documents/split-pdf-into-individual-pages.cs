using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file to be split
        const string inputPdf = "input.pdf";

        // Directory where individual page PDFs will be saved
        const string outputDir = "SplitPages";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Template for the output files.
        // %NUM% will be replaced with the page number (1‑based).
        // Example: c:\temp\SplitPages\page%NUM%.pdf -> page1.pdf, page2.pdf, ...
        string outputTemplate = Path.Combine(outputDir, "page%NUM%.pdf");

        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block.
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Split the PDF into single‑page documents and save them using the template.
            editor.SplitToPages(inputPdf, outputTemplate);
            Console.WriteLine($"PDF split into individual pages under '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
        finally
        {
            // No explicit Dispose needed; just release the reference.
            editor = null;
        }
    }
}