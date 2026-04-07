using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string targetPhrase = "example phrase"; // kept for possible future use

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build a regular‑expression pattern that tolerates variations.
            // This acts as a fuzzy search: it matches words that start with "ex" and "ph"
            // and allows any characters in between.
            string fuzzyPattern = @"\bex\w*?ample\s+ph\w*?rase\b";

            // Configure search options – enable regular‑expression mode and improve matching for encoded fonts
            TextSearchOptions searchOptions = new TextSearchOptions(true)
            {
                UseFontEngineEncoding = true
                // No SearchRegex property – the constructor argument (true) already enables regex mode
            };

            // Use TextFragmentAbsorber for searching. The pattern is passed together with the options.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(fuzzyPattern, searchOptions);

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            if (absorber != null && absorber.TextFragments.Count > 0)
            {
                Console.WriteLine($"Found {absorber.TextFragments.Count} approximate match(es):");
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Display page number, matched text and its location rectangle
                    Console.WriteLine(
                        $"Page {fragment.Page.Number}: \"{fragment.Text}\" " +
                        $"at [{fragment.Rectangle.LLX}, {fragment.Rectangle.LLY}, " +
                        $"{fragment.Rectangle.URX}, {fragment.Rectangle.URY}]");
                }
            }
            else
            {
                Console.WriteLine("No approximate matches found.");
            }
        }
    }
}
