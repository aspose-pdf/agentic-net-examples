using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputTextPath = "output.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize PdfExtractor and bind the PDF document
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);

            // Extract text using UTF-8 encoding
            extractor.ExtractText(Encoding.UTF8);

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                string extractedText = Encoding.UTF8.GetString(ms.ToArray());

                // Save the text to a file with UTF-8 encoding
                File.WriteAllText(outputTextPath, extractedText, Encoding.UTF8);
            }
        }

        Console.WriteLine($"Extracted text saved to '{outputTextPath}'.");
    }
}