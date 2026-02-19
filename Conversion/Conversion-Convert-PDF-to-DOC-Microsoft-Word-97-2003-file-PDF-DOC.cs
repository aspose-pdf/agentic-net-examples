using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output DOC file path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocConverter <input.pdf> <output.doc>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Save the document as DOC. The format is inferred from the .doc extension.
            // This follows the prescribed save rule: {DocumentVar}.Save({OutputPath});
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Conversion successful. DOC saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}