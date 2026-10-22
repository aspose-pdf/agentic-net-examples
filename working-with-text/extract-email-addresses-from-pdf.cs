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

        // Dictionary to hold email addresses per page (page number -> list of emails)
        var emailsByPage = new Dictionary<int, List<string>>();

        // Regular expression for email addresses
        Regex emailRegex = new Regex(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", RegexOptions.Compiled);

        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Extract text from the current page
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[pageNum].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Find all email matches on this page
                var matches = emailRegex.Matches(pageText);
                var emails = new List<string>();
                foreach (Match match in matches)
                {
                    emails.Add(match.Value);
                }

                // Store results (even if empty)
                emailsByPage[pageNum] = emails;
            }
        }

        // Output the collected email addresses
        foreach (var kvp in emailsByPage)
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