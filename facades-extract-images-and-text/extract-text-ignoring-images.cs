using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the PdfExtractor facade
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF document to the extractor
        extractor.BindPdf(inputPdf);

        // Do NOT call ExtractImage() – this ensures images are ignored.
        // The extractor will only process text extraction.

        // Extract text from the PDF
        extractor.ExtractText();

        // Save the extracted text to a file
        extractor.GetText(outputTxt);

        // Release resources
        extractor.Close();

        Console.WriteLine($"Text extraction completed. Output saved to '{outputTxt}'. Images were ignored.");
    }
}