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

        // Verify the input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor within a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Extract text using UTF‑8 encoding
            extractor.ExtractText(Encoding.UTF8);

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);

                // Convert the stream bytes to a UTF‑8 string
                string extractedText = Encoding.UTF8.GetString(ms.ToArray());

                // Write the text to a file with UTF‑8 encoding
                File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
            }
        }

        Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
    }
}