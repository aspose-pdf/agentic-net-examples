using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputCsv = "statistics.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // CSV header
                writer.WriteLine("PageNumber,WordCount");

                // Iterate pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    absorber.Visit(page);               // TextAbsorber.Visit(Page) extracts text
                    string text = absorber.Text ?? string.Empty;

                    // Count words in the extracted text
                    int wordCount = CountWords(text);

                    // Write statistics to CSV
                    writer.WriteLine($"{i},{wordCount}");
                }
            }
        }

        Console.WriteLine($"Word statistics saved to '{outputCsv}'.");
    }

    // Helper method to count words in a string
    static int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        // Split on any whitespace characters, remove empty entries
        string[] words = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}