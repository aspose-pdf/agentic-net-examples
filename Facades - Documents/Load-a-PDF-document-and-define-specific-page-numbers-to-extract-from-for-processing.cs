using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PDF file that will contain the extracted pages
        const string outputPdf = "extracted_pages.pdf";

        // Define the page numbers to extract (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToExtract = new int[] { 2, 4, 7 };

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfFileEditor operates on file paths directly; no Document instance is needed.
            PdfFileEditor editor = new PdfFileEditor();

            // Extract the specified pages and write them to the output file.
            // The method returns true on success, false otherwise.
            bool success = editor.Extract(inputPdf, pagesToExtract, outputPdf);

            Console.WriteLine(success
                ? $"Pages extracted successfully to '{outputPdf}'."
                : "Extraction failed.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}