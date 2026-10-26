using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_input.pdf";
        const string outputTxtPath = "extracted_text.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Determine total pages (1‑based indexing).
        int pageCount;
        using (var doc = new Document(inputPdfPath))
        {
            pageCount = doc.Pages.Count;
        }

        // PdfExtractor implements IDisposable.
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);

            using (var writer = new StreamWriter(outputTxtPath))
            {
                for (int page = 1; page <= pageCount; page++)
                {
                    // Process a single page.
                    extractor.StartPage = page;
                    extractor.EndPage   = page;
                    extractor.ExtractText();

                    // GetText requires a destination stream.
                    string pageText;
                    using (var ms = new MemoryStream())
                    {
                        extractor.GetText(ms);
                        ms.Position = 0; // rewind
                        using (var sr = new StreamReader(ms))
                        {
                            pageText = sr.ReadToEnd();
                        }
                    }

                    writer.WriteLine(pageText);

                    // Progress report (overwrites the same console line).
                    int percent = (page * 100) / pageCount;
                    Console.Write($"\rProcessing page {page}/{pageCount} ({percent}%)");
                }
            }
        }

        // Move to the next line after the progress loop.
        Console.WriteLine("\nExtraction completed successfully.");
    }
}
