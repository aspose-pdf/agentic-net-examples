using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfToDocxConverter <input-pdf> <output-docx>");
            return;
        }

        string inputPdfPath = args[0];
        string outputDocxPath = args[1];

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using the provided load rule)
            Document pdfDocument = new Document(inputPdfPath);

            // Save the document as DOCX.
            // The simple Save method infers the format from the file extension (.docx).
            // This follows the provided document-save rule.
            pdfDocument.Save(outputDocxPath);

            Console.WriteLine($"Conversion successful. DOCX saved to: {outputDocxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}