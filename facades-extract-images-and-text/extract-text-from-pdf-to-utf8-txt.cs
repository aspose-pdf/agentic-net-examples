using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PDF extractor facade
        PdfExtractor extractor = new PdfExtractor();

        // Load the PDF document
        extractor.BindPdf(inputPdf);

        // Extract all text using UTF‑8 encoding
        extractor.ExtractText(Encoding.UTF8);

        // Write the extracted text to a UTF‑8 encoded .txt file
        extractor.GetText(outputTxt);

        // Release resources held by the extractor
        extractor.Close();

        Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
    }
}