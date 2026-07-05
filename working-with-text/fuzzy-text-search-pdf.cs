using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string targetPhrase = "approximate phrase";
        const int maxDistance = 2; // maximum Levenshtein distance for a match

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure search options – the fuzzy flag does not exist on TextSearchOptions,
            // so we perform a normal text search and then apply a Levenshtein distance filter.
            TextSearchOptions searchOptions = new TextSearchOptions(true) // true => ignore case
            {
                UseFontEngineEncoding = true,
                IgnoreShadowText = true
                // FuzzySearch property removed – not available in current Aspose.Pdf versions
            };

            // Create a TextFragmentAbsorber with the target phrase and the configured options.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(targetPhrase, searchOptions);

            // Apply the absorber to all pages of the document.
            doc.Pages.Accept(absorber);

            // Collect fragments that satisfy the fuzzy criteria.
            List<TextFragment> fuzzyMatches = new List<TextFragment>();
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (IsFuzzyMatch(fragment.Text, targetPhrase, maxDistance))
                {
                    fuzzyMatches.Add(fragment);
                }
            }

            // If matches are found, display their text and page numbers.
            if (fuzzyMatches.Count > 0)
            {
                Console.WriteLine($"Found {fuzzyMatches.Count} approximate match(es):");
                foreach (TextFragment fragment in fuzzyMatches)
                {
                    Console.WriteLine($"Page {fragment.Page.Number}: \"{fragment.Text}\"");
                }
            }
            else
            {
                Console.WriteLine("No approximate matches found.");
            }
        }
    }

    // Simple Levenshtein distance implementation for fuzzy matching.
    private static bool IsFuzzyMatch(string source, string target, int maxDistance)
    {
        if (source == null || target == null) return false;
        int distance = LevenshteinDistance(source, target);
        return distance <= maxDistance;
    }

    private static int LevenshteinDistance(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        // Initialize the top and left edges of the matrix.
        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1,      // deletion
                             d[i, j - 1] + 1),     // insertion
                             d[i - 1, j - 1] + cost); // substitution
            }
        }
        return d[n, m];
    }
}
