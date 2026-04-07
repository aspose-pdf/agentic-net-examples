using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "search_log.txt";
        const string phrase    = "sample";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create TextSearchOptions with regular‑expression disabled
            // and enable logging of text‑extraction errors
            TextSearchOptions searchOptions = new TextSearchOptions(false);
            searchOptions.LogTextExtractionErrors = true;

            // Create a TextFragmentAbsorber that searches for the specified phrase
            // and uses the configured search options
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase, searchOptions);

            // Perform the search on the whole document
            doc.Pages.Accept(absorber);

            // Write the search results and any logged errors to a text file
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                // Log the number of matches found
                writer.WriteLine($"Phrase \"{phrase}\" found {absorber.TextFragments.Count} time(s).");

                // If any extraction errors were logged, write their details
                if (absorber.HasErrors)
                {
                    writer.WriteLine("Extraction errors:");
                    foreach (TextExtractionError error in absorber.Errors)
                    {
                        // Use ToString() which provides a readable description of the error
                        writer.WriteLine(error.ToString());
                    }
                }
                else
                {
                    writer.WriteLine("No extraction errors were logged.");
                }
            }

            Console.WriteLine($"Search completed. Log written to \"{logPath}\".");
        }
    }
}