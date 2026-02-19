using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PPTX path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfToPptxConverter <input.pdf> <output.pptx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Save the document as PPTX.
            // Aspose.Pdf determines the output format from the file extension.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}