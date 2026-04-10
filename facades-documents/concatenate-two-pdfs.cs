using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files and the destination file
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "merged.pdf";

        // Verify that both input files exist before attempting concatenation
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {secondPdfPath}");
            return;
        }

        // Create the PdfFileEditor facade and concatenate the two PDFs
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Concatenate(firstPdfPath, secondPdfPath, outputPdfPath);

        // Report the outcome
        if (result)
        {
            Console.WriteLine($"Successfully concatenated PDFs into '{outputPdfPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to concatenate the PDF files.");
        }
    }
}