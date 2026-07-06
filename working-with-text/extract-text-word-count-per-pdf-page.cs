using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "page_word_counts.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare to write CSV output
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                csvWriter.WriteLine("PageNumber,WordCount");

                // Iterate pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    pdfDoc.Pages[pageIndex].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Count words – split on any whitespace characters
                    int wordCount = pageText
                        .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                        .Length;

                    // Write statistics to CSV
                    csvWriter.WriteLine($"{pageIndex},{wordCount}");
                }
            }
        }

        Console.WriteLine($"Word count per page saved to '{outputCsvPath}'.");
    }
}