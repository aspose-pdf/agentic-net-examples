using System;
using System.IO;
using System.Text;
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

        // Extract text from the PDF into a MemoryStream
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Initialize the facade with the source PDF file
            extractor.BindPdf(inputPdf);

            // Extract text using Unicode encoding
            extractor.ExtractText(Encoding.Unicode);

            // Store the extracted text in a memory stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                // Reset the stream position so it can be read by the next component
                textStream.Position = 0;

                // Pass the stream to another library that generates a PDF from text
                GeneratePdfFromTextStream(textStream);
            }

            // Close the facade (optional because of using)
            extractor.Close();
        }
    }

    // Placeholder method representing the downstream PDF generation library.
    // It receives a stream containing the extracted text.
    static void GeneratePdfFromTextStream(Stream textStream)
    {
        // Example: read the text from the stream (the actual library would consume the stream directly)
        using (StreamReader reader = new StreamReader(textStream, Encoding.Unicode, true, 1024, leaveOpen: true))
        {
            string extractedText = reader.ReadToEnd();
            // Call to the external PDF generator would go here, e.g.:
            // ExternalPdfGenerator.CreatePdfFromText(extractedText);
            Console.WriteLine($"Extracted text length: {extractedText.Length}");
        }
    }
}