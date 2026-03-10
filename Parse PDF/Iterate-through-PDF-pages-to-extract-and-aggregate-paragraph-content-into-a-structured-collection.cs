using System;
using System.Collections.Generic;
using System.IO;
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

        // Dictionary to hold page number -> list of paragraph texts
        var documentParagraphs = new Dictionary<int, List<string>>();

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a ParagraphAbsorber and search the current page
                ParagraphAbsorber absorber = new ParagraphAbsorber();
                absorber.Visit(page);

                // The absorber stores results in PageMarkups collection
                // For a single page visit there will be exactly one PageMarkup
                PageMarkup markup = absorber.PageMarkups[0];

                // Prepare list to collect paragraph texts for this page
                var paragraphsOnPage = new List<string>();

                // Iterate through each paragraph found on the page
                foreach (MarkupParagraph paragraph in markup.Paragraphs)
                {
                    // Use the Text property which concatenates all fragments of the paragraph
                    string paragraphText = paragraph.Text ?? string.Empty;
                    paragraphsOnPage.Add(paragraphText);
                }

                // Store the collected paragraphs in the dictionary
                documentParagraphs[pageIndex] = paragraphsOnPage;
            }
        }

        // Example output: display aggregated paragraphs per page
        foreach (var kvp in documentParagraphs)
        {
            Console.WriteLine($"Page {kvp.Key}:");
            int paraNum = 1;
            foreach (string text in kvp.Value)
            {
                Console.WriteLine($"  Paragraph {paraNum++}: {text}");
            }
            Console.WriteLine();
        }
    }
}