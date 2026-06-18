using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputTxt = "extracted.txt";      // destination text file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor (Facade) to extract text.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file.
            extractor.BindPdf(inputPdf);

            // Extract text using UTF‑8 encoding (Unicode characters are preserved).
            extractor.ExtractText(Encoding.UTF8);

            // Retrieve the extracted text into a memory stream.
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);                 // writes text to the stream
                ms.Position = 0;                       // reset position for reading

                // Convert stream bytes to a UTF‑8 string.
                string extractedText = Encoding.UTF8.GetString(ms.ToArray());

                // Write the string to a file with explicit UTF‑8 encoding.
                File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
            }
        }

        Console.WriteLine($"Text extracted to '{outputTxt}'.");
    }
}