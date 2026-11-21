using System;
using System.IO;
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

        // PdfExtractor does NOT implement IDisposable, so no using block is required.
        PdfExtractor extractor = new PdfExtractor();

        // Bind the source PDF file.
        extractor.BindPdf(inputPdf);

        // Perform the extraction of text only. Do NOT call ExtractImage().
        extractor.ExtractText();

        // Retrieve the extracted text via a stream (GetText requires a destination stream).
        string extractedText;
        using (MemoryStream textStream = new MemoryStream())
        {
            extractor.GetText(textStream); // write text to the stream
            textStream.Position = 0;
            using (StreamReader reader = new StreamReader(textStream))
            {
                extractedText = reader.ReadToEnd();
            }
        }

        // Save the text to a file.
        File.WriteAllText(outputTxt, extractedText);

        Console.WriteLine($"Text extracted to '{outputTxt}' (images ignored).");
    }
}
