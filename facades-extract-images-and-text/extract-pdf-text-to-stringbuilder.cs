using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTextPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // StringBuilder to hold extracted text for further manipulation
        StringBuilder sb = new StringBuilder();

        // Use PdfExtractor (Facade) to extract text from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract all text using Unicode encoding (default)
            extractor.ExtractText();

            // Save extracted text to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                ms.Position = 0; // reset stream position

                // Convert stream bytes to string (Unicode)
                string extracted = Encoding.Unicode.GetString(ms.ToArray());

                // Append to StringBuilder for any further processing
                sb.Append(extracted);
            }
        }

        // Example manipulation: trim and normalize line endings
        string processed = sb.ToString()
                             .Trim()
                             .Replace("\r\n", "\n");

        // Write the final text to disk using Unicode encoding
        File.WriteAllText(outputTextPath, processed, Encoding.Unicode);

        Console.WriteLine($"Text extracted and saved to '{outputTextPath}'.");
    }
}
