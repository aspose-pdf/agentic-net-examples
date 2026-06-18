using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfExtractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPath);

            // Extract text using Unicode encoding
            extractor.ExtractText(Encoding.Unicode);

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                // Convert the memory stream to a string
                string allText = Encoding.Unicode.GetString(ms.ToArray());

                // Aspose.Pdf uses form‑feed '\f' as the page delimiter
                string[] pageTexts = allText.Split('\f');

                for (int i = 0; i < pageTexts.Length; i++)
                {
                    string pageText = pageTexts[i].Trim();
                    Console.WriteLine($"--- Page {i + 1} ---");
                    Console.WriteLine(pageText);
                    // Optional: save each page's text to a separate file
                    // File.WriteAllText($"page_{i + 1}.txt", pageText);
                }
            }
        }
    }
}