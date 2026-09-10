using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output text file path (only text will be extracted)
        const string outputTxt = "extracted_text.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfExtractor (Facade API)
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF document to the extractor
        extractor.BindPdf(inputPdf);

        // -----------------------------------------------------------------
        // Configure extraction: do NOT call ExtractImage().
        // By omitting the ExtractImage() call, the extractor will ignore
        // all images and only process text extraction.
        // -----------------------------------------------------------------

        // Extract text from the PDF
        extractor.ExtractText();

        // Save the extracted text to a file
        extractor.GetText(outputTxt);

        // Release resources
        extractor.Close();

        Console.WriteLine($"Text extraction completed. Images were ignored. Output saved to '{outputTxt}'.");
    }
}