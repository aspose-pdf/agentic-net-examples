using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "protected.pdf";   // Path to the password‑protected PDF
        const string outputTxt = "extracted.txt";   // Destination for extracted text
        const string ownerPwd  = "owner123";        // Owner password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfExtractor implements IDisposable – wrap in using for deterministic cleanup
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Provide the owner password before binding the document
                extractor.Password = ownerPwd;

                // Bind the encrypted PDF file
                extractor.BindPdf(inputPdf);

                // Extract all text (Unicode encoding is the default)
                extractor.ExtractText();

                // Write the extracted text to a file
                extractor.GetText(outputTxt);
            }

            Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}