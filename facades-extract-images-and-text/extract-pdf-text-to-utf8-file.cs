using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Use PdfExtractor (Facade) to bind and extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);

            // Extract text using UTF-8 (Unicode) encoding
            extractor.ExtractText(Encoding.UTF8);

            // Capture extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                string extractedText = Encoding.UTF8.GetString(ms.ToArray());

                // Save the text to a file with UTF-8 encoding
                File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
            }
        }

        Console.WriteLine($"Text successfully extracted to '{outputTxtPath}'.");
    }
}