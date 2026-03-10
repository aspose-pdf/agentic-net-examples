using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string headingToFind = "Chapter 1"; // change as needed; leave empty to get all text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (global rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Extract text from the current page (text‑extraction rule)
                TextAbsorber absorber = new TextAbsorber();
                absorber.Visit(page);
                string pageText = absorber.Text;

                if (!string.IsNullOrEmpty(headingToFind))
                {
                    // Find the specified heading on the page (simple string search)
                    int headingPos = pageText.IndexOf(headingToFind, StringComparison.OrdinalIgnoreCase);
                    if (headingPos >= 0)
                    {
                        // Return text starting from the heading to the end of the page
                        string relevantText = pageText.Substring(headingPos);
                        Console.WriteLine($"Page {pageIndex} – Text under heading \"{headingToFind}\":");
                        Console.WriteLine(relevantText);
                    }
                    else
                    {
                        Console.WriteLine($"Page {pageIndex} – Heading \"{headingToFind}\" not found.");
                    }
                }
                else
                {
                    // No heading filter – output full page text
                    Console.WriteLine($"Page {pageIndex} – Full text:");
                    Console.WriteLine(pageText);
                }
            }
        }
    }
}