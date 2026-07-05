using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted_text.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize PdfExtractor and bind the PDF document
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);

            // Do NOT call ExtractImage() – this effectively ignores all images.
            // Only extract text from the PDF.
            extractor.ExtractText();

            // Save the extracted text to a file.
            extractor.GetText(outputTxt);
        }

        Console.WriteLine($"Text extraction completed. Output saved to '{outputTxt}'. Images were ignored.");
    }
}