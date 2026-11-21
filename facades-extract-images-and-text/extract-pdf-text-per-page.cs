using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – wrap in using for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Extract all text from the document
            extractor.ExtractText();

            // GetText requires a destination stream. Write the extracted text to a MemoryStream
            string fullText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0;        // rewind for reading
                using (StreamReader reader = new StreamReader(textStream))
                {
                    fullText = reader.ReadToEnd();
                }
            }

            // Split the text into individual pages using the page delimiter (form‑feed \f)
            string[] pageTexts = fullText.Split(new[] { '\f' }, StringSplitOptions.RemoveEmptyEntries);

            // Write each page's text to a separate .txt file
            for (int i = 0; i < pageTexts.Length; i++)
            {
                string pageFile = Path.Combine(outputDir, $"Page_{i + 1}.txt");
                File.WriteAllText(pageFile, pageTexts[i]);
                Console.WriteLine($"Extracted text for page {i + 1} to '{pageFile}'.");
            }
        }
    }
}
