using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Extract text from the PDF and store it in a MemoryStream
        using (MemoryStream textStream = new MemoryStream())
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF file
                extractor.BindPdf(inputPdf);

                // Extract all text using Unicode encoding (default)
                extractor.ExtractText();

                // Write the extracted text into the MemoryStream
                extractor.GetText(textStream);
            }

            // Reset the stream position before reading or passing it further
            textStream.Position = 0;

            // Optional: read the extracted text as a string for verification
            string extractedText = new StreamReader(textStream).ReadToEnd();
            Console.WriteLine($"Extracted text length: {extractedText.Length}");

            // Pass the MemoryStream to another library that generates a PDF from text
            // Example placeholder (replace with actual library call):
            // OtherPdfGenerator.GeneratePdfFromText(textStream);
        }
    }
}