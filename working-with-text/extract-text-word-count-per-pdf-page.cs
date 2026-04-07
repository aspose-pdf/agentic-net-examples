using System;
using System.IO;
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

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare CSV writer
                using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    csvWriter.WriteLine("PageNumber,WordCount");

                    // Iterate over all pages (1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                    {
                        // Extract text from the current page
                        TextAbsorber absorber = new TextAbsorber();
                        pdfDoc.Pages[pageIndex].Accept(absorber);
                        string pageText = absorber.Text ?? string.Empty;

                        // Count words (split on whitespace, ignore empty entries)
                        int wordCount = pageText.Split(
                            new char[] { ' ', '\t', '\r', '\n' },
                            StringSplitOptions.RemoveEmptyEntries
                        ).Length;

                        // Write statistics to CSV
                        csvWriter.WriteLine($"{pageIndex},{wordCount}");
                    }
                }
            }

            Console.WriteLine($"Word count per page saved to '{outputCsvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}