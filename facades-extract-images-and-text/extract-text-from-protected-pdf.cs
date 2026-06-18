using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "protected.pdf";
        const string outputTxt = "extracted.txt";
        const string ownerPwd  = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfExtractor implements IDisposable, so wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Provide the owner password for the encrypted PDF
                extractor.Password = ownerPwd;

                // Bind the encrypted PDF file
                extractor.BindPdf(inputPdf);

                // Extract all text (Unicode encoding is default)
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