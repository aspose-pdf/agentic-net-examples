using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "output.txt";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract all text using UTF‑8 encoding
            extractor.ExtractText(Encoding.UTF8);

            // Write the extracted text to a UTF‑8 encoded .txt file
            extractor.GetText(outputTxtPath);
        }

        Console.WriteLine($"Text extraction complete. Output saved to '{outputTxtPath}'.");
    }
}