using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a PDF path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        // Check that the file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor from Aspose.Pdf.Facades to extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(pdfPath);

            // Extract text from all pages
            extractor.ExtractText();

            // Retrieve the extracted text via the stream overload
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0; // rewind for reading
                using (StreamReader reader = new StreamReader(textStream))
                {
                    string extractedText = reader.ReadToEnd();
                    // Output the text to the console
                    Console.WriteLine(extractedText);
                }
            }
        }
    }
}
