using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "search_results.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Phrase to search (approximate/fuzzy match)
            string phrase = "sample phrase";

            // Configure fuzzy search options – the constructor that takes a bool enables fuzzy search
            TextSearchOptions searchOptions = new TextSearchOptions(true);

            // Prepare the output file
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                int totalMatches = 0;
                writer.WriteLine($"Searching for \"{phrase}\" with fuzzy matching …");

                // Iterate through all pages because TextFragment does not expose a PageNumber property in newer versions
                for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
                {
                    // Create a new absorber for each page (required to get page‑specific results)
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
                    absorber.TextSearchOptions = searchOptions;

                    // Apply the absorber to the current page
                    doc.Pages[pageIdx].Accept(absorber);

                    // Process found fragments
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        totalMatches++;
                        // Rectangle of the found text fragment
                        Aspose.Pdf.Rectangle rect = fragment.Rectangle;
                        writer.WriteLine($"Page {pageIdx}: Rect [{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}]");
                    }
                }

                writer.WriteLine($"Found {totalMatches} matches for \"{phrase}\" (fuzzy).");
            }

            Console.WriteLine($"Search results written to '{outputPath}'.");
        }
    }
}
