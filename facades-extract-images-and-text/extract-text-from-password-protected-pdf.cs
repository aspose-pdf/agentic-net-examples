using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "protected.pdf";
        const string outputTxt = "extracted.txt";
        const string ownerPassword = "ownerpass";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfExtractor handles decryption when the correct password is supplied.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.Password = ownerPassword;   // Owner password for the protected PDF
            extractor.BindPdf(inputPdf);          // Load the PDF file
            extractor.ExtractText();              // Perform text extraction
            extractor.GetText(outputTxt);         // Save extracted text to a file
        }

        Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
    }
}