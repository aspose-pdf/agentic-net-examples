using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfExtractor (Aspose.Pdf.Facades)
        PdfExtractor extractor = new PdfExtractor();

        // Load the PDF document
        extractor.BindPdf(inputPdf);

        // Extract all text from the document
        extractor.ExtractText();

        // Retrieve the extracted text via a stream (required overload)
        string extractedText;
        using (MemoryStream textStream = new MemoryStream())
        {
            extractor.GetText(textStream); // write text to the stream
            textStream.Position = 0; // rewind for reading
            using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
            {
                extractedText = reader.ReadToEnd();
            }
        }

        // Save the text to a file using UTF‑8 encoding for international characters
        File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);

        Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
    }
}