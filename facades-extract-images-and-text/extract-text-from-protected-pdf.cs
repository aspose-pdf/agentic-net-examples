using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "protected.pdf";
        const string outputTxt = "extracted.txt";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfExtractor implements IDisposable – ensure deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Supply the owner password before binding the PDF
            extractor.Password = ownerPassword;

            // Bind the encrypted PDF document
            extractor.BindPdf(inputPdf);

            // Perform text extraction (Unicode encoding is default)
            extractor.ExtractText();

            // Write the extracted text to a file
            extractor.GetText(outputTxt);
        }

        Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
    }
}