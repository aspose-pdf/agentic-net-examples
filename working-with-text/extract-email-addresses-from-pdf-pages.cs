using System;
using System.IO;
using System.Collections.Generic;
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

        // Regular expression that matches typical e‑mail addresses
        Regex emailRegex = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", RegexOptions.IgnoreCase);

        // Dictionary to hold page‑wise matches
        var results = new Dictionary<int, List<string>>();

        // Open the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create an absorber that searches using the e‑mail regex
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(emailRegex, new TextSearchOptions(true));

                // Perform the search on the current page
                doc.Pages[i].Accept(absorber);

                // Gather all matched text fragments
                var pageMatches = new List<string>();
                foreach (TextFragment tf in absorber.TextFragments)
                {
                    pageMatches.Add(tf.Text);
                }

                if (pageMatches.Count > 0)
                {
                    results[i] = pageMatches;
                }
            }
        }

        // Display the collected e‑mail addresses
        foreach (var kvp in results)
        {
            Console.WriteLine($"Page {kvp.Key}:");
            foreach (var email in kvp.Value)
            {
                Console.WriteLine($"  {email}");
            }
        }
    }
}