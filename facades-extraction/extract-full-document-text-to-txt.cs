using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "FullDocumentExport.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Extract full document text using PdfExtractor (Aspose.Pdf.Facades)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file
            extractor.BindPdf(inputPdf);

            // Perform text extraction for all pages
            extractor.ExtractText();

            // Get the extracted text via the stream overload
            string fullText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // writes text to the stream
                textStream.Position = 0; // rewind
                using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                {
                    fullText = reader.ReadToEnd();
                }
            }

            // Save the extracted text directly to a .txt file
            File.WriteAllText(outputTxt, fullText);
        }

        Console.WriteLine($"Extraction complete. Text saved to '{outputTxt}'.");
    }
}