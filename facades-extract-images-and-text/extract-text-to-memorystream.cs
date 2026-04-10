using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Initialize the PdfExtractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Extract text using Unicode encoding
            extractor.ExtractText(Encoding.Unicode);

            // Create a memory stream to hold the extracted text
            using (MemoryStream textStream = new MemoryStream())
            {
                // Save the extracted text into the memory stream
                extractor.GetText(textStream);

                // Reset the stream position for subsequent reading
                textStream.Position = 0;

                // Example: read the text (optional, for verification)
                using (StreamReader reader = new StreamReader(textStream, Encoding.Unicode))
                {
                    string extractedText = reader.ReadToEnd();
                    Console.WriteLine($"Extracted text length: {extractedText.Length}");
                }

                // Reset again before passing to another library
                textStream.Position = 0;

                // Pass the memory stream to an external PDF generation library
                // ExternalPdfGenerator.CreatePdf(textStream, "generated.pdf");
                // The above line is a placeholder for the actual library call.
            }
        }
    }
}