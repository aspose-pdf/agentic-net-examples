using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Detect headings (simple heuristic: font size >= 16 points)
            // -----------------------------------------------------------------
            var headings = new List<(string Text, int PageNumber)>();

            for (int i = 1; i <= doc.Pages.Count; i++)               // 1‑based indexing
            {
                Page page = doc.Pages[i];

                // Paragraphs collection contains TextFragment objects
                foreach (TextFragment fragment in page.Paragraphs)
                {
                    // Simple heading detection based on font size
                    if (fragment.TextState != null && fragment.TextState.FontSize >= 16)
                    {
                        headings.Add((fragment.Text.Trim(), i));
                    }
                }
            }

            // If no headings were found, just save the original document
            if (headings.Count == 0)
            {
                Console.WriteLine("No headings detected – saving original document.");
                doc.Save(outputPath);
                return;
            }

            // -----------------------------------------------------------------
            // 2. Insert a new page at the beginning for the Table of Contents
            // -----------------------------------------------------------------
            // Insert returns the newly created page positioned at index 1
            Page tocPage = doc.Pages.Insert(1);

            // Title for the TOC
            TextFragment title = new TextFragment("Table of Contents")
            {
                TextState = { FontSize = 24, FontStyle = FontStyles.Bold, ForegroundColor = Aspose.Pdf.Color.Black }
            };
            // Position the title near the top of the page
            title.Position = new Position(50, tocPage.PageInfo.Height - 50);
            tocPage.Paragraphs.Add(title);

            // -----------------------------------------------------------------
            // 3. Add each heading as an entry with a link to its page
            // -----------------------------------------------------------------
            const double leftMargin = 50;
            const double lineHeight = 20;
            double currentY = tocPage.PageInfo.Height - 100; // start below the title

            foreach (var (text, pageNum) in headings)
            {
                // Create the visible text for the entry
                TextFragment entry = new TextFragment($"{text} .................................. {pageNum}")
                {
                    TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.DarkBlue }
                };
                entry.Position = new Position(leftMargin, currentY);
                tocPage.Paragraphs.Add(entry);

                // Approximate rectangle covering the text (used for the link)
                // Width is estimated; height is based on font size
                double rectWidth = 400; // enough to cover the entry text
                double rectHeight = entry.TextState.FontSize + 2;
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                    leftMargin,
                    currentY - rectHeight,
                    leftMargin + rectWidth,
                    currentY
                );

                // Create a link annotation that jumps to the target page
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect)
                {
                    Action = new GoToAction(pageNum) // page numbers are 1‑based
                };
                tocPage.Annotations.Add(link);

                // Move to next line
                currentY -= lineHeight;
            }

            // -----------------------------------------------------------------
            // 4. Save the modified document
            // -----------------------------------------------------------------
            doc.Save(outputPath);
            Console.WriteLine($"Document with Table of Contents saved to '{outputPath}'.");
        }
    }
}