using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableOfContents.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -----------------------------------------------------------------
            // 1. Create a Table of Contents (TOC) page
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // Title for TOC page
            TextFragment tocTitle = new TextFragment("Table of Contents")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold }
            };
            tocTitle.Position = new Position(50, 750);
            tocPage.Paragraphs.Add(tocTitle);

            // -----------------------------------------------------------------
            // 2. Create section pages with headings
            // -----------------------------------------------------------------
            // Example: three sections
            string[] sectionTitles = { "Introduction", "Usage Guidelines", "Conclusion" };
            int[] sectionPageNumbers = new int[sectionTitles.Length]; // will hold page numbers

            for (int i = 0; i < sectionTitles.Length; i++)
            {
                // Add a new page for the section
                Page secPage = doc.Pages.Add();

                // Record the page number (1‑based indexing)
                sectionPageNumbers[i] = doc.Pages.Count; // current last page

                // Add a heading to the section page
                TextFragment heading = new TextFragment(sectionTitles[i])
                {
                    TextState = { FontSize = 18, FontStyle = FontStyles.Bold }
                };
                heading.Position = new Position(50, 750);
                secPage.Paragraphs.Add(heading);

                // Add some placeholder body text
                TextFragment body = new TextFragment("Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                                                      "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.")
                {
                    TextState = { FontSize = 12 }
                };
                body.Position = new Position(50, 720);
                secPage.Paragraphs.Add(body);
            }

            // -----------------------------------------------------------------
            // 3. Populate the TOC page with entries linked to the sections
            // -----------------------------------------------------------------
            const double startY = 700;   // starting Y coordinate for first entry
            const double lineHeight = 20;
            double currentY = startY;

            for (int i = 0; i < sectionTitles.Length; i++)
            {
                // Create a visible TOC entry
                TextFragment entry = new TextFragment($"{i + 1}. {sectionTitles[i]}")
                {
                    TextState = { FontSize = 14, ForegroundColor = Color.Blue, Underline = true }
                };
                entry.Position = new Position(60, currentY);
                tocPage.Paragraphs.Add(entry);

                // Approximate width based on character count; adjust as needed.
                double entryWidth = entry.TextState.FontSize * entry.Text.Length * 0.5;
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                    entry.Position.XIndent,
                    entry.Position.YIndent,
                    entry.Position.XIndent + entryWidth,
                    entry.Position.YIndent + entry.TextState.FontSize);

                // Create a link annotation that jumps to the target page
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect)
                {
                    Action = new GoToAction(doc.Pages[sectionPageNumbers[i]])
                };

                // Add the annotation to the TOC page
                tocPage.Annotations.Add(link);

                // Move to next line
                currentY -= lineHeight;
            }

            // -----------------------------------------------------------------
            // 4. Save the PDF document
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable Table of Contents saved to '{outputPath}'.");
    }
}
