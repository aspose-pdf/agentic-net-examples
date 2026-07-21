using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the edited PDF that contains annotations
        const string inputPdf = "edited.pdf";

        // Template for the output files.
        // %NUM% will be replaced with the page number (1‑based).
        const string outputTemplate = "output_page%NUM%.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfFileEditor provides the SplitToPages method which creates
        // a separate PDF file for each page, preserving all page content
        // including annotations.
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // This overload writes the split pages directly to disk using
        // the provided filename template.
        pdfEditor.SplitToPages(inputPdf, outputTemplate);

        Console.WriteLine("PDF has been split into individual pages.");
    }
}