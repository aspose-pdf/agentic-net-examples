using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // Path to source PDF
        const string outputTxtPath = "output.txt";  // Path for extracted text

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // CREATE – initialize the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // LOAD – bind the PDF document to the extractor
                extractor.BindPdf(inputPdfPath);

                // EXTRACT – extract text using UTF‑8 encoding
                extractor.ExtractText(Encoding.UTF8);

                // SAVE – write the extracted text to a UTF‑8 encoded .txt file
                extractor.GetText(outputTxtPath);
            }

            Console.WriteLine($"Text extracted successfully to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Extraction failed: {ex.Message}");
        }
    }
}