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

        // Regular expression to match email addresses
        Regex emailRegex = new Regex(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", RegexOptions.Compiled);

        // Store emails found per page
        var emailsByPage = new Dictionary<int, List<string>>();

        // Load PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Extract text from the current page using TextAbsorber
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[i].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Find all email matches on this page
                var matches = emailRegex.Matches(pageText);
                var list = new List<string>();
                foreach (Match m in matches)
                {
                    list.Add(m.Value);
                }

                if (list.Count > 0)
                {
                    emailsByPage[i] = list;
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