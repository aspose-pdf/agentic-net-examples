using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // StringBuilder will hold the extracted text for further processing
        StringBuilder sb = new StringBuilder();

        // PdfExtractor (Aspose.Pdf.Facades) is used for text extraction
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file
            extractor.BindPdf(inputPath);

            // Extract text from all pages
            extractor.ExtractText();

            // Get the extracted text via a stream (correct overload)
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0;        // rewind for reading
                using (StreamReader reader = new StreamReader(textStream))
                {
                    string extractedText = reader.ReadToEnd();
                    sb.Append(extractedText);
                }
            }
        }

        // Example manipulation: normalize line endings and trim whitespace
        string finalText = sb.ToString()
                             .Replace("\r\n", "\n")
                             .Trim();

        // Write the manipulated text to a file
        File.WriteAllText(outputPath, finalText);

        Console.WriteLine($"Text extracted and saved to '{outputPath}'.");
    }
}
