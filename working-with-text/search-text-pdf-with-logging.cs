using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "search_log.txt";
        const string searchPhrase = "hello";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule for lifecycle management)
        using (Document doc = new Document(inputPath))
        {
            // Configure text search options with logging enabled
            TextSearchOptions searchOptions = new TextSearchOptions(true) // regular expression mode (true) – not required but allowed
            {
                LogTextExtractionErrors = true // enable logging of extraction errors
            };

            // Create a TextFragmentAbsorber for the desired phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase)
            {
                TextSearchOptions = searchOptions
            };

            // Perform the search on the whole document
            absorber.Visit(doc); // uses the Visit(Document) method

            // Prepare log content
            using (StreamWriter logWriter = new StreamWriter(logPath, false))
            {
                logWriter.WriteLine($"Search Phrase: \"{searchPhrase}\"");
                logWriter.WriteLine($"Total Matches Found: {absorber.TextFragments.Count}");

                // Log any extraction errors if they were captured
                if (absorber.HasErrors)
                {
                    logWriter.WriteLine("Extraction Errors:");
                    foreach (var error in absorber.Errors)
                    {
                        // TextExtractionError does not expose PageNumber or Message; use its string representation
                        logWriter.WriteLine($"- {error}");
                    }
                }
                else
                {
                    logWriter.WriteLine("No extraction errors recorded.");
                }
            }

            // (Optional) Save the document unchanged to demonstrate save rule usage
            doc.Save(outputPath);
        }

        Console.WriteLine($"Search completed. Log written to '{logPath}'.");
    }
}
