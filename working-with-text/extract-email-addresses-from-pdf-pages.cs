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

        // Dictionary to hold email addresses per page (1‑based page index)
        var emailsByPage = new Dictionary<int, List<string>>();

        // Regular expression for email addresses
        string emailPattern = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}";

        // Open the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Create a TextFragmentAbsorber that searches using the regex pattern
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(
                    new Regex(emailPattern),
                    new TextSearchOptions(true) // enable regular‑expression search
                );

                // Apply the absorber to the current page
                doc.Pages[pageNum].Accept(absorber);

                // Collect found email addresses from this page
                var emails = new List<string>();
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The fragment text is the matched email address
                    emails.Add(fragment.Text);
                }

                // Store results if any were found
                if (emails.Count > 0)
                {
                    emailsByPage[pageNum] = emails;
                }
            }
        }

        // Output the collected email addresses
        foreach (var kvp in emailsByPage)
        {
            Console.WriteLine($"Page {kvp.Key}:");
            foreach (string email in kvp.Value)
            {
                Console.WriteLine($"  {email}");
            }
        }
    }
}