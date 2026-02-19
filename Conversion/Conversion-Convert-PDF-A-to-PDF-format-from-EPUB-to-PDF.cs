using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input EPUB file path and output PDF file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <input.epub> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input EPUB file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the EPUB document using default load options
            Document pdfDocument = new Document(inputPath, new EpubLoadOptions());

            // Save the document as a regular PDF file
            pdfDocument.Save(outputPath);

            Console.WriteLine($"EPUB successfully converted to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}