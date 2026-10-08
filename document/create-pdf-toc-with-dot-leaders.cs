using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output file path (adjust as needed)
        const string outputPath = "TableOfContents.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // -----------------------------------------------------------------
            // 1. Create a page that will hold the Table of Contents (TOC)
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // Determine safe Y‑coordinates based on the page height to avoid out‑of‑range errors
            double pageHeight = tocPage.PageInfo.Height;
            double marginTop = 50;               // distance from top edge
            double currentY = pageHeight - marginTop; // start position for the title

            // Add a title for the TOC
            TextFragment tocTitle = new TextFragment("Table of Contents")
            {
                TextState =
                {
                    FontSize = 20,
                    FontStyle = FontStyles.Bold,
                    Font = FontRepository.FindFont("Arial") ?? FontRepository.FindFont("Helvetica")
                },
                Position = new Position(50, currentY)
            };
            tocPage.Paragraphs.Add(tocTitle);

            // -----------------------------------------------------------------
            // 2. Create content sections and collect their titles and page numbers
            // -----------------------------------------------------------------
            // NOTE: Aspose.Pdf evaluation mode allows a maximum of 4 pages. To stay within the limit we use three sections.
            string[] sectionTitles = { "Introduction", "Getting Started", "Advanced Topics" };
            var tocEntries = new List<(string Title, int PageNumber)>();

            foreach (string title in sectionTitles)
            {
                // Add a new page for each section
                Page contentPage = doc.Pages.Add();

                // Record the page number (Aspose.Pdf uses 1‑based indexing)
                int pageNumber = doc.Pages.Count; // current last page index

                // Add a heading on the page (positioned safely within page bounds)
                double headingY = contentPage.PageInfo.Height - 50; // 50 points from top
                TextFragment heading = new TextFragment(title)
                {
                    TextState =
                    {
                        FontSize = 18,
                        FontStyle = FontStyles.Bold,
                        Font = FontRepository.FindFont("Arial") ?? FontRepository.FindFont("Helvetica")
                    },
                    Position = new Position(50, headingY)
                };
                contentPage.Paragraphs.Add(heading);

                // Add some placeholder body text (a little lower than the heading)
                double bodyY = headingY - 30;
                TextFragment body = new TextFragment(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.")
                {
                    TextState =
                    {
                        FontSize = 12,
                        Font = FontRepository.FindFont("Arial") ?? FontRepository.FindFont("Helvetica")
                    },
                    Position = new Position(50, bodyY)
                };
                contentPage.Paragraphs.Add(body);

                // Store the entry for later TOC generation
                tocEntries.Add((title, pageNumber));
            }

            // -----------------------------------------------------------------
            // 3. Generate the TOC entries with custom dot leaders
            // -----------------------------------------------------------------
            // Starting vertical position for the first entry (some space below the title)
            double entryY = currentY - 60; // 60 points below the title
            const double lineSpacing = 20;

            foreach (var entry in tocEntries)
            {
                // Compute dot leader string so that the total line length is consistent
                const int totalLength = 60; // total characters for visual alignment
                string pageStr = entry.PageNumber.ToString();
                int dotCount = totalLength - entry.Title.Length - pageStr.Length;
                if (dotCount < 2) dotCount = 2; // ensure at least a couple of dots
                string dots = new string('.', dotCount);
                string line = $"{entry.Title}{dots}{pageStr}";

                TextFragment tocLine = new TextFragment(line)
                {
                    TextState =
                    {
                        FontSize = 12,
                        Font = FontRepository.FindFont("Arial") ?? FontRepository.FindFont("Helvetica")
                    },
                    Position = new Position(50, entryY)
                };
                tocPage.Paragraphs.Add(tocLine);

                entryY -= lineSpacing; // move down for the next entry
            }

            // -----------------------------------------------------------------
            // 4. Save the PDF (no SaveOptions needed because output is PDF)
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom TOC saved to '{Path.GetFullPath(outputPath)}'.");
    }
}
