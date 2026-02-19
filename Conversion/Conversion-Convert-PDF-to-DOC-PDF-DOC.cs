using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output DOC path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocConverter <input.pdf> <output.doc>");
            return;
        }

        string inputPdfPath = args[0];
        string outputDocPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Save as DOC. The file extension determines the output format.
            // This uses the built‑in save logic (no explicit options) as required by the lifecycle rule.
            pdfDocument.Save(outputDocPath);

            Console.WriteLine($"Conversion successful. DOC saved to: {outputDocPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}