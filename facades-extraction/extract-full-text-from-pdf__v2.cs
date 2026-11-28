using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "FullText.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Extract all text from the document
            extractor.ExtractText();

            // GetText has no parameter‑less overload – write the text to a stream
            string text;
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms); // writes extracted text into the stream
                ms.Position = 0;       // rewind for reading
                using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                {
                    text = reader.ReadToEnd();
                }
            }

            // Save the extracted text to a file
            File.WriteAllText(outputTxt, text, Encoding.UTF8);
        }

        Console.WriteLine($"Full document text extracted to '{outputTxt}'.");
    }
}
