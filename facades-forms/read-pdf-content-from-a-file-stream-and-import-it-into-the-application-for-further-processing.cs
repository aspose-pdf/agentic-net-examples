using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file as a read‑only stream.
        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
        {
            // Bind the PDF stream to a PdfExtractor facade.
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(pdfStream);

            // Extract all text from the PDF.
            extractor.ExtractText();

            // GetText requires a destination stream. Write the extracted text to a MemoryStream
            // and then read it back as a string.
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

            Console.WriteLine("Extracted text:");
            Console.WriteLine(extractedText);
        }
    }
}
