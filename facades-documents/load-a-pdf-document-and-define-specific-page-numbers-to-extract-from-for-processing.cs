using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the extracted output PDF
        const string inputPdf  = "source.pdf";
        const string outputPdf = "extracted_pages.pdf";

        // Define the page numbers you want to extract (1‑based indexing)
        int[] pagesToExtract = new int[] { 2, 4, 7 };

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Extract the specified pages and save them as a new PDF
        bool success = editor.Extract(inputPdf, pagesToExtract, outputPdf);

        if (success)
            Console.WriteLine($"Pages {string.Join(", ", pagesToExtract)} extracted to '{outputPdf}'.");
        else
            Console.Error.WriteLine("Extraction failed.");
    }
}