using System;
using System.IO;
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

        try
        {
            // Open the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare CSV writer
                using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    csvWriter.WriteLine("Page,WordCount");

                    // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                    for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                    {
                        // Extract text from the current page
                        TextAbsorber absorber = new TextAbsorber();
                        absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                        pdfDoc.Pages[pageNumber].Accept(absorber);
                        string pageText = absorber.Text ?? string.Empty;

                        // Count words (split on whitespace, ignore empty entries)
                        int wordCount = 0;
                        if (!string.IsNullOrWhiteSpace(pageText))
                        {
                            string[] words = pageText.Split(
                                new char[] { ' ', '\t', '\r', '\n' },
                                StringSplitOptions.RemoveEmptyEntries);
                            wordCount = words.Length;
                        }

                        // Write statistics for this page
                        csvWriter.WriteLine($"{pageNumber},{wordCount}");
                    }
                }

                Console.WriteLine($"Word count statistics saved to '{outputCsvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}