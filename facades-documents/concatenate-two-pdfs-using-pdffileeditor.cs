using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Verify that the source files exist before attempting concatenation.
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
            // PdfFileEditor does not implement IDisposable, so a plain instance is sufficient.
            PdfFileEditor editor = new PdfFileEditor();

            // Use the two‑file overload to concatenate the PDFs.
            bool result = editor.Concatenate(firstPdf, secondPdf, outputPdf);

            if (result)
                Console.WriteLine($"Successfully concatenated to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Concatenation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}