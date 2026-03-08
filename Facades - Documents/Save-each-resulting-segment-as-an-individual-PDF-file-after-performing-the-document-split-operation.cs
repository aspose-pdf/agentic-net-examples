using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file to be split
        const string inputPdf = "input.pdf";

        // Output file name template – %NUM% will be replaced with the page number (1‑based)
        const string outputTemplate = "output_page%NUM%.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // PdfFileEditor provides the SplitToPages method that both splits the document
        // and saves each page as an individual PDF file according to the template.
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the split operation. The method handles saving; no additional Save calls are needed.
        editor.SplitToPages(inputPdf, outputTemplate);

        Console.WriteLine("Document split completed. Individual pages saved as separate PDF files.");
    }
}