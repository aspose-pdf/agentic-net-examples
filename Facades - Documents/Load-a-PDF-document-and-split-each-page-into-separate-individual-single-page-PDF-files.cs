using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";

        // Template for the output files.
        // %NUM% will be replaced with the page number (1‑based).
        const string outputTemplate = "output_page%NUM%.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfFileEditor provides the SplitToPages method that writes each page
            // to a separate PDF file according to the template.
            PdfFileEditor editor = new PdfFileEditor();

            // This overload directly saves the split pages to disk.
            editor.SplitToPages(inputPdf, outputTemplate);

            Console.WriteLine("PDF successfully split into individual pages.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}