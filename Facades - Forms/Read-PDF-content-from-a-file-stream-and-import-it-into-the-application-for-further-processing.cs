using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the PdfExtractor facade and bind the stream
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(pdfStream);

            // Extract text from the bound PDF
            extractor.ExtractText();

            // Get the extracted text via the overload that writes to a Stream
            string extractedText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0;        // rewind for reading
                using (StreamReader reader = new StreamReader(textStream))
                {
                    extractedText = reader.ReadToEnd();
                }
            }

            Console.WriteLine("=== Extracted PDF Text ===");
            Console.WriteLine(extractedText);
        }
    }
}
