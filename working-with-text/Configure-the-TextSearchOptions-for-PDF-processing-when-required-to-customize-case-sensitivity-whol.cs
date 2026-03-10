using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Configure TextSearchOptions (common options)
            // ------------------------------------------------------------
            // The constructor requires at least one argument (e.g., enable searching in annotations).
            TextSearchOptions searchOptions = new TextSearchOptions(true)
            {
                // Example: include text inside annotations (already true via ctor, but kept for clarity)
                SearchInAnnotations = true,
                // Example: ignore shadow text (optional)
                IgnoreShadowText = false,
                // Example: limit search to page bounds
                LimitToPageBounds = true
            };

            // ------------------------------------------------------------
            // 2. Case‑insensitive whole‑word search using a regular expression
            // ------------------------------------------------------------
            // Pattern: whole word "example" (case‑insensitive)
            string pattern = @"\bexample\b";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // Create a TextFragmentAbsorber with the regex and the configured options
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(regex, searchOptions);

            // ------------------------------------------------------------
            // 3. Search direction
            // ------------------------------------------------------------
            // Forward search (pages 1 .. Count)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Accept(absorber);
            }

            // If a reverse (backward) search is required, uncomment the block below:
            /*
            for (int i = doc.Pages.Count; i >= 1; i--)
            {
                doc.Pages[i].Accept(absorber);
            }
            */

            // ------------------------------------------------------------
            // 4. Process found fragments (example: replace with "sample")
            // ------------------------------------------------------------
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = "sample";
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
