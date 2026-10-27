using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "page_word_counts.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a CSV writer
            using (StreamWriter csvWriter = new StreamWriter(outputCsv))
            {
                // Write CSV header
                csvWriter.WriteLine("PageNumber,WordCount");

                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber
                    {
                        ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                    };
                    doc.Pages[i].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Count words by splitting on whitespace
                    int wordCount = 0;
                    if (!string.IsNullOrWhiteSpace(pageText))
                    {
                        string[] words = Regex.Split(pageText, @"\s+");
                        foreach (string w in words)
                        {
                            if (!string.IsNullOrEmpty(w))
                                wordCount++;
                        }
                    }

                    // Write the statistics for this page
                    csvWriter.WriteLine($"{i},{wordCount}");
                }
            }
        }

        Console.WriteLine($"Word count per page written to '{outputCsv}'.");
    }
}