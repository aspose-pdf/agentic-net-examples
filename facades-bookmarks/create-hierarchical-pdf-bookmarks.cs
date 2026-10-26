using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // for GoToAction and ExplicitDestinationType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Collect chapter titles and their page numbers.
        // Simple heuristic: a line that starts with "Chapter" is considered a title.
        List<(string Title, int Page)> chapters = new List<(string, int)>();

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Scan each page for chapter headings.
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based page indexing
            {
                TextAbsorber absorber = new TextAbsorber
                {
                    ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                };
                doc.Pages[i].Accept(absorber);
                string pageText = absorber.Text;

                string[] lines = pageText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (line.StartsWith("Chapter", StringComparison.OrdinalIgnoreCase))
                    {
                        chapters.Add((line.Trim(), i));
                        break; // assume first matching line per page is the chapter title
                    }
                }
            }

            // Add hierarchical bookmarks (level 1 = top‑level).
            foreach (var (title, page) in chapters)
            {
                // Create a GoTo action that points to the target page.
                Page targetPage = doc.Pages[page];
                GoToAction goTo = new GoToAction(targetPage, ExplicitDestinationType.Fit);

                OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
                {
                    Title = title,
                    Action = goTo
                };
                doc.Outlines.Add(bookmark);
            }

            // Save the PDF with the new bookmarks.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
