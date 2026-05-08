using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "word_counts.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document (lifecycle: using ensures disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare CSV output
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                csvWriter.WriteLine("PageNumber,WordCount");

                // Iterate pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    page.Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Count words using a simple word regex
                    int wordCount = Regex.Matches(pageText, @"\b\w+\b").Count;

                    // Write statistics to CSV
                    csvWriter.WriteLine($"{pageIndex},{wordCount}");
                }
            }
        }

        Console.WriteLine($"Word count per page saved to '{outputCsvPath}'.");
    }
}
