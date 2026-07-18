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
        const string targetPhrase = "example phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Suppress unused‑variable warning for targetPhrase (it is kept for illustration).
        _ = targetPhrase;

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure search options:
            // - Enable regular‑expression mode (allows flexible pattern matching)
            // - Use font‑engine encoding to improve matching on documents with non‑standard encodings
            TextSearchOptions searchOptions = new TextSearchOptions(true)
            {
                UseFontEngineEncoding = true,
                IgnoreShadowText = true
            };

            // Build a fuzzy regular‑expression pattern.
            // The pattern below matches the target phrase with possible extra or missing characters
            // between the words (e.g., "example   phrase", "examp1e phrase", etc.).
            string fuzzyPattern = @"ex\w*?ample\s*?phrase";

            // Perform the fuzzy search using TextFragmentAbsorber.
            // The absorber will collect all matching fragments.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(fuzzyPattern, searchOptions);
            doc.Pages.Accept(absorber);

            // Output the found fragments
            if (absorber != null && absorber.TextFragments.Count > 0)
            {
                Console.WriteLine($"Found {absorber.TextFragments.Count} approximate match(es):");
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($" - Page {fragment.Page.Number}: \"{fragment.Text}\"");
                }

                // Example modification: highlight each found fragment by changing its background color
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.TextState.BackgroundColor = Color.Yellow;
                }
            }
            else
            {
                Console.WriteLine("No approximate matches found.");
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
