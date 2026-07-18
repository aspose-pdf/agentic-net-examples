using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (password‑protected) and owner password
        const string inputPdf  = "protected.pdf";
        const string ownerPwd  = "ownerPassword";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfExtractor is a Facade that supports password handling via the Password property
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Provide the owner password before binding the PDF
            extractor.Password = ownerPwd;

            // Bind the encrypted PDF file
            extractor.BindPdf(inputPdf);

            // Extract all text using Unicode encoding (default)
            extractor.ExtractText();

            // Save the extracted text to a file
            extractor.GetText(outputTxt);
        }

        Console.WriteLine($"Text extracted to '{outputTxt}'.");
    }
}