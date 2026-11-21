using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a PDF path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: ExtractText <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        // Check that the file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfExtractor is the Facades class for extracting content
            PdfExtractor extractor = new PdfExtractor();

            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Extract text from all pages
            extractor.ExtractText();

            // Retrieve the extracted text via the overload that writes to a stream
            string extractedText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0; // rewind for reading
                using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                {
                    extractedText = reader.ReadToEnd();
                }
            }

            // Output the text to the console
            Console.WriteLine(extractedText);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting text: {ex.Message}");
        }
    }
}
