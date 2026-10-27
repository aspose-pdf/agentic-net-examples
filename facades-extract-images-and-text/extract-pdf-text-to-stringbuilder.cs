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

        // StringBuilder to hold the extracted text for further manipulation
        StringBuilder sb = new StringBuilder();

        // Use PdfExtractor from Aspose.Pdf.Facades to extract text
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPath);
        extractor.ExtractText();

        // Get the extracted text via a stream (PdfExtractor.GetText requires a Stream)
        using (MemoryStream textStream = new MemoryStream())
        {
            extractor.GetText(textStream); // write text to the stream
            textStream.Position = 0;        // rewind for reading
            using (StreamReader reader = new StreamReader(textStream))
            {
                sb.Append(reader.ReadToEnd());
            }
        }

        // Example of further manipulation (optional)
        // sb.Replace("\r\n", "\n");

        // Write the StringBuilder content to a file
        File.WriteAllText(outputPath, sb.ToString());

        Console.WriteLine($"Text extracted and saved to '{outputPath}'.");
    }
}