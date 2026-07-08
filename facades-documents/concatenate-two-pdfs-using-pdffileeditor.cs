using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides in this namespace

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        // Output PDF file
        const string outputPdf = "merged.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is required
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the two PDFs into the output file
            // This overload returns true if the operation succeeded
            bool success = editor.Concatenate(firstPdf, secondPdf, outputPdf);

            if (success)
                Console.WriteLine($"Successfully concatenated '{firstPdf}' and '{secondPdf}' into '{outputPdf}'.");
            else
                Console.Error.WriteLine("Concatenation failed.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., file access issues)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}