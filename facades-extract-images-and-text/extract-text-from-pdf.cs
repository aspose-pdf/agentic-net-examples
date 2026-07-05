using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a PDF path argument is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <executable> <pdfPath>");
            return;
        }

        string pdfPath = args[0];

        // Verify the file exists before processing
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfExtractor implements IDisposable; use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor facade
            extractor.BindPdf(pdfPath);

            // Perform text extraction using Unicode encoding (default)
            extractor.ExtractText();

            // Capture the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                ms.Position = 0; // Reset stream position before reading

                // Convert the byte array to a string using Unicode encoding
                string extractedText = Encoding.Unicode.GetString(ms.ToArray());

                // Output the extracted text to the console
                Console.WriteLine(extractedText);
            }
        }
    }
}