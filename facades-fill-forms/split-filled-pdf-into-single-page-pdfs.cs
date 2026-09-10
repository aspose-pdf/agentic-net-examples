using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the filled PDF that needs to be split
        const string inputPdfPath = "filled_document.pdf";

        // Directory where the single‑page PDFs will be saved
        const string outputDirectory = "SplitPages";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor pdfEditor = new PdfFileEditor();

            // Build a file name template that includes the %NUM% placeholder.
            // The placeholder will be replaced with the page number (starting at 1).
            string fileNameTemplate = Path.Combine(outputDirectory, "page%NUM%.pdf");

            // Split the PDF into single‑page documents and save them using the template.
            // This method creates one PDF file per page: page1.pdf, page2.pdf, etc.
            pdfEditor.SplitToPages(inputPdfPath, fileNameTemplate);

            Console.WriteLine($"Successfully split '{inputPdfPath}' into individual pages under '{outputDirectory}'.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., file access issues)
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}