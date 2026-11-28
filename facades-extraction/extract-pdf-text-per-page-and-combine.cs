using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "PageTexts";
        const string combinedFile = "CombinedPages.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the folder for per‑page text files exists
        Directory.CreateDirectory(outputFolder);

        // Get total page count using Document (PdfExtractor does not expose PageCount)
        int totalPages = new Document(inputPdf).Pages.Count;

        // Extract text per page using PdfExtractor (IDisposable)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);

            for (int page = 1; page <= totalPages; page++)
            {
                // Set the range to a single page
                extractor.StartPage = page;
                extractor.EndPage   = page;
                extractor.ExtractText();

                // GetText writes the extracted text to a stream – read it back as a string
                string pageText;
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms); // writes text to the stream
                    pageText = Encoding.UTF8.GetString(ms.ToArray());
                }

                string pageFile = Path.Combine(outputFolder, $"Page_{page}.txt");
                File.WriteAllText(pageFile, pageText ?? string.Empty);
            }
        }

        // Concatenate all per‑page text files into a single file
        using (StreamWriter writer = new StreamWriter(combinedFile, false))
        {
            for (int page = 1; page <= totalPages; page++)
            {
                string pageFile = Path.Combine(outputFolder, $"Page_{page}.txt");
                if (!File.Exists(pageFile))
                    continue; // safety, should not happen

                string content = File.ReadAllText(pageFile);
                writer.WriteLine(content);
                // Optional separator between pages
                writer.WriteLine($"\n--- End of Page {page} ---\n");
            }
        }

        Console.WriteLine($"All pages extracted and combined into '{combinedFile}'.");
    }
}
