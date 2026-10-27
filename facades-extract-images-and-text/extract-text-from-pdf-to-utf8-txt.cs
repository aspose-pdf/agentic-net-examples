using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string txtPath = "output.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block.
        using (var extractor = new PdfExtractor())
        {
            // Load the PDF document.
            extractor.BindPdf(pdfPath);

            // Extract all text from the PDF.
            extractor.ExtractText();

            // Get the extracted text via a stream (PdfExtractor.GetText requires a Stream).
            string extractedText;
            using (var textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0; // rewind for reading
                using (var reader = new StreamReader(textStream, Encoding.UTF8))
                {
                    extractedText = reader.ReadToEnd();
                }
            }

            // Write the text to a UTF‑8 encoded file.
            File.WriteAllText(txtPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{txtPath}'.");
    }
}
