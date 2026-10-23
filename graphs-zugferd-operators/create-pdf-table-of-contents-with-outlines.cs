using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable automatic tagging so headings are detected as HeaderElements
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the source PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content
            ITaggedContent taggedContent = doc.TaggedContent;

            // Find all heading elements (HeaderElement) recursively
            var headings = taggedContent.RootElement.FindElements<HeaderElement>(true);

            // Sort headings by their page number to preserve document order
            var sortedHeadings = new List<HeaderElement>(headings);
            sortedHeadings.Sort((h1, h2) =>
            {
                int p1 = h1.Page?.Number ?? int.MaxValue;
                int p2 = h2.Page?.Number ?? int.MaxValue;
                return p1.CompareTo(p2);
            });

            // Insert a new blank page at the beginning for the Table of Contents
            Page tocPage = doc.Pages.Insert(1); // creates a new empty page at position 1

            // Add a title for the TOC
            TextFragment title = new TextFragment("Table of Contents");
            title.TextState.FontSize = 20;
            title.TextState.FontStyle = FontStyles.Bold;
            title.Position = new Position(50, 800);
            tocPage.Paragraphs.Add(title);

            // Vertical offset for TOC entries
            double yOffset = 750;

            // Process each heading
            foreach (HeaderElement heading in sortedHeadings)
            {
                // Retrieve heading text and page number (fallback to 0 if not found)
                string headingText = heading.ActualText ?? "Untitled";
                int pageNumber = heading.Page?.Number ?? 0; // 1‑based page index

                if (pageNumber == 0)
                    continue; // skip headings that are not associated with a page

                // Create a TOC entry line
                string tocLine = $"{headingText} ................................ {pageNumber}";
                TextFragment entry = new TextFragment(tocLine);
                entry.Position = new Position(50, yOffset);
                tocPage.Paragraphs.Add(entry);
                yOffset -= 20; // move down for next entry

                // Add a clickable outline (bookmark) that jumps to the heading page
                // Use a FitExplicitDestination (fits the whole page) and set outline properties
                OutlineItemCollection outline = new OutlineItemCollection(doc.Outlines)
                {
                    Title = headingText,
                    Bold = true,
                    Italic = false,
                    Color = System.Drawing.Color.DarkBlue,
                    Destination = new FitExplicitDestination(heading.Page)
                };
                doc.Outlines.Add(outline);
            }

            // Save the modified document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Table of Contents saved to '{outputPath}'.");
    }
}
