using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be read
        const string pdfPath = "sample.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
        {
            // Create a PdfExtractor facade – it does not implement IDisposable,
            // so we do not wrap it in a using block.
            PdfExtractor extractor = new PdfExtractor();

            // Bind the PDF stream to the extractor for further processing
            extractor.BindPdf(pdfStream);

            // Extract all text from the PDF
            extractor.ExtractText();

            // Retrieve the extracted text via the overload that writes to a Stream
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

            // Output the extracted text (or pass it to other processing logic)
            Console.WriteLine("Extracted Text:");
            Console.WriteLine(extractedText);
        }
    }
}
