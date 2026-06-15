using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "statistics.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load PDF document (using rule: document-disposal-with-using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare CSV writer
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false, Encoding.UTF8))
            {
                // CSV header
                csvWriter.WriteLine("PageNumber,WordCount");

                // Iterate pages (1‑based indexing per rule)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Extract text from the current page (rule: use TextAbsorber)
                    TextAbsorber absorber = new TextAbsorber();
                    page.Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Simple word count: split on any whitespace
                    int wordCount = 0;
                    if (!string.IsNullOrWhiteSpace(pageText))
                    {
                        string[] words = pageText.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                        wordCount = words.Length;
                    }

                    // Write statistics for this page
                    csvWriter.WriteLine($"{pageIndex},{wordCount}");
                }
            }
        }

        Console.WriteLine($"Word count statistics saved to '{outputCsvPath}'.");
    }
}