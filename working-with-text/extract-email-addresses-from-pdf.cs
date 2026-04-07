using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Regex pattern for email addresses
        Regex emailRegex = new Regex(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b",
                                    RegexOptions.IgnoreCase);

        // Dictionary to hold page number -> list of matched email strings
        var emailResults = new Dictionary<int, List<string>>();

        // Load PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Create absorber that uses the regex; enable regex search via TextSearchOptions
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(
                    emailRegex,
                    new TextSearchOptions(true));

                // Apply absorber to the current page
                doc.Pages[pageNum].Accept(absorber);

                // Retrieve matched fragments for the regex
                TextFragmentCollection fragments = absorber.RegexResults[emailRegex];

                // Store matches
                var matches = new List<string>();
                foreach (TextFragment fragment in fragments)
                {
                    matches.Add(fragment.Text);
                }

                emailResults[pageNum] = matches;
            }
        }

        // Output results
        foreach (var kvp in emailResults)
        {
            Console.WriteLine($"Page {kvp.Key}:");
            if (kvp.Value.Count == 0)
            {
                Console.WriteLine("  No email addresses found.");
            }
            else
            {
                foreach (string email in kvp.Value)
                {
                    Console.WriteLine($"  {email}");
                }
            }
        }
    }
}
