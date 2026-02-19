using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output DOCX path.
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: PdfToDocxConverter <input.pdf> <output.docx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPath);

            // Save the document as DOCX. The format is inferred from the .docx extension.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Conversion successful. DOCX saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}