using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfExtractor facade
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF document to the extractor
        extractor.BindPdf(inputPdf);

        // Extract text using UTF‑8 encoding
        extractor.ExtractText(Encoding.UTF8);

        // Save the extracted text to a UTF‑8 encoded .txt file
        extractor.GetText(outputTxt);

        // Release resources held by the extractor
        extractor.Close();

        Console.WriteLine($"Text extracted to '{outputTxt}'.");
    }
}