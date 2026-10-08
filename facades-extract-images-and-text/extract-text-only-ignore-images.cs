using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_text.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create the PdfExtractor facade and bind the source PDF
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPath);

            // Enable text extraction only (do NOT call ExtractImage())
            extractor.ExtractText();

            // Extract the text into a memory stream, then read it as a string
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0;        // rewind for reading
                using (StreamReader reader = new StreamReader(textStream))
                {
                    string extractedText = reader.ReadToEnd();
                    File.WriteAllText(outputPath, extractedText);
                }
            }

            Console.WriteLine($"Text extracted (images ignored) and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
