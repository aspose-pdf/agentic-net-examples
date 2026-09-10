using System;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Regular expression for email addresses
            Regex emailRegex = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", RegexOptions.IgnoreCase);
            TextSearchOptions searchOptions = new TextSearchOptions(true); // enable regex search

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create an absorber for the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(emailRegex, searchOptions);
                doc.Pages[i].Accept(absorber);

                // Output each found email with its page number
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"Page {i}: {fragment.Text}");
                }
            }
        }
    }
}