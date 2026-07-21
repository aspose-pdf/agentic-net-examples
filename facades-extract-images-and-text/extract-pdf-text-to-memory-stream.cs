using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to a source PDF file (for demo purposes)
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file into a memory stream
        using (FileStream fileStream = File.OpenRead(inputPath))
        using (MemoryStream pdfStream = new MemoryStream())
        {
            fileStream.CopyTo(pdfStream);
            pdfStream.Position = 0; // reset to beginning

            // Initialize the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF from the memory stream
                extractor.BindPdf(pdfStream);

                // Perform text extraction
                extractor.ExtractText();

                // Create a memory stream to receive the extracted text
                using (MemoryStream textStream = new MemoryStream())
                {
                    // Write extracted text into the stream
                    extractor.GetText(textStream);
                    textStream.Position = 0; // reset for reading

                    // Example: read the text as a Unicode string for further processing
                    string extractedText = System.Text.Encoding.Unicode.GetString(textStream.ToArray());
                    Console.WriteLine($"Extracted text length: {extractedText.Length}");

                    // At this point 'textStream' contains the raw text data and can be used
                    // for any in‑memory processing without touching the file system.
                }
            }
        }
    }
}