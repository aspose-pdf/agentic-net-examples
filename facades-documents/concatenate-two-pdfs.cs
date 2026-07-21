using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        // Output PDF file path
        const string outputPdf = "merged.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"Source file not found: {firstPdf}");
            return;
        }

        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"Source file not found: {secondPdf}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is required
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the two PDFs into a new file
            bool result = editor.Concatenate(firstPdf, secondPdf, outputPdf);

            if (result)
                Console.WriteLine($"PDFs successfully concatenated into '{outputPdf}'.");
            else
                Console.Error.WriteLine("Concatenation failed.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}