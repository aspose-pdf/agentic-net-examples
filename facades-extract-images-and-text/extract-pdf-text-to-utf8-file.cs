using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputTxtPath = "extracted.txt"; // destination text file

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Use PdfExtractor (Facade) to extract text with UTF‑8 encoding
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract text using UTF‑8 (Unicode) encoding
            extractor.ExtractText(Encoding.UTF8);

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);                     // write text to stream
                string extractedText = Encoding.UTF8.GetString(ms.ToArray());

                // Save the text to a file with UTF‑8 encoding
                File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
            }
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}