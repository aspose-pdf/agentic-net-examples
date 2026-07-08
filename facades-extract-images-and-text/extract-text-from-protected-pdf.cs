using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (password‑protected) and output text file paths
        const string inputPdf  = "protected.pdf";
        const string outputTxt = "extracted.txt";

        // Owner password for the PDF (full‑access password)
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create the extractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Provide the owner password before binding the PDF
                extractor.Password = ownerPassword;

                // Bind the encrypted PDF file
                extractor.BindPdf(inputPdf);

                // Extract all text using Unicode encoding (default)
                extractor.ExtractText();

                // Save the extracted text to a file
                extractor.GetText(outputTxt);
            }

            Console.WriteLine($"Text extracted successfully to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}