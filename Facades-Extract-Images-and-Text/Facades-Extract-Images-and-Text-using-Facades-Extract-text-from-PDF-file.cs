using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output text file path
        const string outputTxtPath = "extracted_text.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract all text using Unicode encoding (default)
            extractor.ExtractText();

            // Save the extracted text to a file
            extractor.GetText(outputTxtPath);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}