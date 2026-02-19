using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument or default)
        string inputPdfPath = args.Length > 0 ? args[0] : "input.pdf";
        // Output DOCX path (second argument or default)
        string outputDocxPath = args.Length > 1 ? args[1] : "output.docx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Save the document as DOCX
            pdfDocument.Save(outputDocxPath, SaveFormat.DocX);

            Console.WriteLine($"Conversion successful. DOCX saved to '{outputDocxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}