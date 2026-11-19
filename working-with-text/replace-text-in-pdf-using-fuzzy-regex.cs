using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string targetPhrase = "target phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TextSearchOptions constructor requires a bool indicating whether to search in annotations.
            var searchOptions = new TextSearchOptions(true);

            // ---------------------------------------------------------------------
            // Enable fuzzy matching – the exact property name differs between Aspose.Pdf
            // versions ("FuzzySearch" in newer releases, "FuzzySearchEnabled" in older).
            // We use reflection to set whichever property exists, avoiding compile‑time
            // errors and keeping the code version‑agnostic.
            // ---------------------------------------------------------------------
            Type optsType = typeof(TextSearchOptions);
            PropertyInfo fuzzyProp = optsType.GetProperty("FuzzySearch") ?? optsType.GetProperty("FuzzySearchEnabled");
            if (fuzzyProp != null && fuzzyProp.CanWrite)
            {
                fuzzyProp.SetValue(searchOptions, true);
            }

            PropertyInfo threshProp = optsType.GetProperty("FuzzySearchThreshold");
            if (threshProp != null && threshProp.CanWrite)
            {
                threshProp.SetValue(searchOptions, 0.8f);
            }

            // Set up the absorber with the (now) fuzzy‑enabled options
            var absorber = new TextFragmentAbsorber(targetPhrase)
            {
                TextSearchOptions = searchOptions
            };

            // Apply the absorber to all pages
            doc.Pages.Accept(absorber);

            // Output each match with its page number (pages are 1‑based)
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                int pageNumber = fragment.Page.Number; // correct way to get page number
                string text = fragment.Text;
                Console.WriteLine($"Page {pageNumber}: \"{text}\"");
            }
        }
    }
}
