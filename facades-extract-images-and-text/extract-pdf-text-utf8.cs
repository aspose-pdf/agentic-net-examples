using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Use PdfExtractor from Aspose.Pdf.Facades to extract text
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(doc);
            extractor.ExtractText();

            // Retrieve the extracted text via the stream overload
            string extractedText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0;        // rewind for reading
                using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                {
                    extractedText = reader.ReadToEnd();
                }
            }

            // Write the text to a file with UTF‑8 encoding to preserve international characters
            File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted and saved to '{outputTxtPath}'.");
    }
}
