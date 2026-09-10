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
            // Open the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare CSV writer
                using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    csvWriter.WriteLine("PageNumber,WordCount");

                    // Iterate pages (1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                    {
                        Page page = pdfDoc.Pages[pageIndex];

                        // Extract text from the current page
                        TextAbsorber absorber = new TextAbsorber();
                        page.Accept(absorber);
                        string pageText = absorber.Text ?? string.Empty;

                        // Count words (split on whitespace, ignore empty entries)
                        int wordCount = 0;
                        if (!string.IsNullOrWhiteSpace(pageText))
                        {
                            string[] words = pageText.Split(
                                (char[])null, // split on any whitespace
                                StringSplitOptions.RemoveEmptyEntries);
                            wordCount = words.Length;
                        }

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