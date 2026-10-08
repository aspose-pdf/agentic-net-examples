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
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Create Heading objects with the correct constructor and style usage
            // -----------------------------------------------------------------
            var headings = new List<Heading>
            {
                new Heading(1)
                {
                    Text = "Chapter 1 – Introduction",
                    IsAutoSequence = true,
                    Style = NumberingStyle.NumeralsArabic // decimal (1, 2, 3, …)
                },
                new Heading(1)
                {
                    Text = "Chapter 2 – Background",
                    IsAutoSequence = true,
                    Style = NumberingStyle.NumeralsRomanUppercase // I, II, III, …
                },
                new Heading(2)
                {
                    Text = "Section 2.1 – History",
                    IsAutoSequence = true,
                    Style = NumberingStyle.LettersLowercase // a, b, c, …
                },
                new Heading(2)
                {
                    Text = "Section 2.2 – Current State",
                    IsAutoSequence = true,
                    Style = NumberingStyle.LettersUppercase // A, B, C, …
                },
                new Heading(1)
                {
                    Text = "Appendix A – References",
                    IsAutoSequence = true,
                    Style = NumberingStyle.NumeralsRomanLowercase // i, ii, iii, …
                }
            };

            // ---------------------------------------------------------------
            // 2. Build a simple TOC page using the core Aspose.Pdf API
            // ---------------------------------------------------------------
            // Insert a new page at the beginning of the document
            Page tocPage = doc.Pages.Insert(1);
            // Starting vertical position (top of the page)
            float yPos = 800;
            foreach (var heading in headings)
            {
                // Indent according to the heading level
                string indent = new string(' ', (heading.Level - 1) * 4);
                // Create a text fragment for the TOC entry
                TextFragment tf = new TextFragment(indent + heading.Text)
                {
                    Position = new Position(50, yPos),
                    // Reduce font size for deeper levels (optional visual cue)
                    TextState = { FontSize = 12 - (heading.Level - 1) }
                };
                tocPage.Paragraphs.Add(tf);
                yPos -= 20; // Move down for the next entry
            }

            // ---------------------------------------------------------------
            // 3. (Optional) Add bookmarks to the document outline for navigation
            // ---------------------------------------------------------------
            foreach (var heading in headings)
            {
                // Create a bookmark (outline item) for each heading.
                // In a real scenario you would also set the Destination to the page
                // where the heading actually appears.
                OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
                {
                    Title = heading.Text,
                    Open = true // expanded by default; set false to collapse
                };
                doc.Outlines.Add(bookmark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with structured Table of Contents saved to '{outputPath}'.");
    }
}
