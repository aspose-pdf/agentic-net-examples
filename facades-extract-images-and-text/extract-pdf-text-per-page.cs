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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Extract text from all pages
            extractor.ExtractText();

            // Get the concatenated text; pages are separated by a form‑feed character (\f)
            string allText;
            using (MemoryStream textStream = new MemoryStream())
            {
                // Write extracted text into the stream
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream))
                {
                    allText = reader.ReadToEnd();
                }
            }

            // Split the text into individual pages using the delimiter
            string[] pageTexts = allText.Split('\f');

            // Write each page's text to a separate .txt file
            for (int i = 0; i < pageTexts.Length; i++)
            {
                string pageContent = pageTexts[i].Trim();

                // Skip empty entries that may appear due to leading/trailing delimiters
                if (string.IsNullOrEmpty(pageContent))
                    continue;

                string outPath = Path.Combine(outputDir, $"Page_{i + 1}.txt");
                File.WriteAllText(outPath, pageContent);
                Console.WriteLine($"Saved page {i + 1} text to '{outPath}'.");
            }
        }
    }
}
