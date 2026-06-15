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

        // Verify that input files exist
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

        // Create the PdfFileEditor facade
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate the two PDFs into the output file
        bool result = editor.Concatenate(firstPdf, secondPdf, outputPdf);

        if (result)
        {
            Console.WriteLine($"Successfully concatenated '{firstPdf}' and '{secondPdf}' into '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed.");
        }
    }
}