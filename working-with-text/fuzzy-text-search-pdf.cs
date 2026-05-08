using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Simple Levenshtein distance implementation for fuzzy matching
    static int LevenshteinDistance(string s, string t)
    {
        if (string.IsNullOrEmpty(s)) return t?.Length ?? 0;
        if (string.IsNullOrEmpty(t)) return s.Length;

        int[,] d = new int[s.Length + 1, t.Length + 1];

        for (int i = 0; i <= s.Length; i++) d[i, 0] = i;
        for (int j = 0; j <= t.Length; j++) d[0, j] = j;

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 1; j <= t.Length; j++)
            {
                int cost = s[i - 1] == t[j - 1] ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }

        return d[s.Length, t.Length];
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string targetPhrase = "hello world";   // phrase we want to find approximately
        const int maxDistance = 2;                  // allowed Levenshtein distance for a match

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextFragmentAbsorber without an initial phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // TextSearchOptions now requires a constructor argument (e.g., search in annotations)
            TextSearchOptions searchOptions = new TextSearchOptions(true)
            {
                UseFontEngineEncoding = true
            };
            absorber.TextSearchOptions = searchOptions;

            // Search every page
            doc.Pages.Accept(absorber);

            // Collect fragments whose text is within the fuzzy distance threshold
            List<TextFragment> fuzzyMatches = new List<TextFragment>();
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                string fragmentText = fragment.Text?.Trim() ?? string.Empty;
                int distance = LevenshteinDistance(fragmentText, targetPhrase);
                if (distance <= maxDistance)
                {
                    fuzzyMatches.Add(fragment);
                }
            }

            // Output the results
            Console.WriteLine($"Found {fuzzyMatches.Count} approximate match(es) for \"{targetPhrase}\":");
            foreach (TextFragment match in fuzzyMatches)
            {
                Console.WriteLine($"Page {match.Page.Number}, Text: \"{match.Text}\"");
            }
        }
    }
}
