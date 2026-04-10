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
        // %NUM% will be replaced with the page number (starting from 1)
        const string outputTemplate = "output_page%NUM%.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we do NOT use a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into individual pages and save each page using the template.
        // This will create files like output_page1.pdf, output_page2.pdf, etc.
        editor.SplitToPages(inputPdf, outputTemplate);

        Console.WriteLine("PDF successfully split into individual pages.");
    }
}