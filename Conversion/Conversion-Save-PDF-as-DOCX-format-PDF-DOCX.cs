using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for SaveFormat enum

class PdfToDocxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output DOCX path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToDocxConverter <input.pdf> <output.docx>");
            return;
        }

        string inputPdfPath = args[0];
        string outputDocxPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Save the document as DOCX using the built‑in SaveFormat enumeration
            pdfDocument.Save(outputDocxPath, SaveFormat.DocX);

            Console.WriteLine($"Successfully converted '{inputPdfPath}' to '{outputDocxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}