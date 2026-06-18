using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        const string inputPdf = "edited.pdf";               // source PDF with annotations
        const string outputFolder = "SplitPages";           // folder for single‑page PDFs

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Template for output files – %NUM% will be replaced by the page number (1‑based)
        string fileNameTemplate = Path.Combine(outputFolder, "page%NUM%.pdf");

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into single‑page documents; each page (including its annotations) is saved
            // to a separate file according to the template above.
            editor.SplitToPages(inputPdf, fileNameTemplate);

            Console.WriteLine($"PDF split into individual pages at '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}