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

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfExtractor implements IDisposable – wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(inputPdf);

                // Extract text using UTF‑8 encoding
                extractor.ExtractText(Encoding.UTF8);

                // Save the extracted text to a UTF‑8 encoded .txt file
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