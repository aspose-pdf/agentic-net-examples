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

        // Dictionary to hold email addresses found on each page (page number -> list of emails)
        var emailsByPage = new Dictionary<int, List<string>>();

        // Regular expression for email addresses
        Regex emailPattern = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", RegexOptions.IgnoreCase);

        // TextFragmentAbsorber configured to use regular expressions
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(emailPattern, new TextSearchOptions(true));

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Apply the absorber to the current page
                page.Accept(absorber);

                // Collect matched email addresses from this page
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

                // Reset absorber for the next page
                absorber.Reset();
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