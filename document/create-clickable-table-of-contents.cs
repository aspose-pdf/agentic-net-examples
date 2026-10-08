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

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // -----------------------------------------------------------------
            // 1. Create a Table of Contents (TOC) page.
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();
            // Add a title for the TOC.
            TextFragment tocTitle = new TextFragment("Table of Contents")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold, ForegroundColor = Aspose.Pdf.Color.Black },
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Position = new Position(0, 800) // Y coordinate from top of page.
            };
            tocPage.Paragraphs.Add(tocTitle);

            // -----------------------------------------------------------------
            // 2. Create content sections on separate pages.
            //    Store a reference to each section page for the links.
            // -----------------------------------------------------------------
            const int sectionCount = 3;
            Page[] sectionPages = new Page[sectionCount];
            for (int i = 0; i < sectionCount; i++)
            {
                // Add a new page for the section.
                Page sectionPage = doc.Pages.Add();
                sectionPages[i] = sectionPage;

                // Add a heading for the section.
                TextFragment heading = new TextFragment($"Section {i + 1}")
                {
                    TextState = { FontSize = 18, FontStyle = FontStyles.Bold, ForegroundColor = Aspose.Pdf.Color.DarkBlue },
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Position = new Position(0, 750)
                };
                sectionPage.Paragraphs.Add(heading);

                // Add some placeholder body text.
                TextFragment body = new TextFragment($"This is the content of section {i + 1}.")
                {
                    TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black },
                    Position = new Position(50, 700)
                };
                sectionPage.Paragraphs.Add(body);
            }

            // -----------------------------------------------------------------
            // 3. Populate the TOC page with entries and link annotations.
            // -----------------------------------------------------------------
            const double startY = 700; // Starting Y coordinate for the first entry.
            const double lineHeight = 30;
            for (int i = 0; i < sectionCount; i++)
            {
                // Text for the TOC entry.
                string entryText = $"Section {i + 1}";
                TextFragment entry = new TextFragment(entryText)
                {
                    TextState = { FontSize = 14, ForegroundColor = Aspose.Pdf.Color.Blue },
                    Position = new Position(100, startY - i * lineHeight)
                };
                tocPage.Paragraphs.Add(entry);

                // Approximate width: font size * number of characters * 0.5 (simple heuristic).
                double textWidth = entry.TextState.FontSize * entryText.Length * 0.5;
                double textHeight = entry.TextState.FontSize + 2;

                // Define the rectangle for the link annotation.
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                    entry.Position.XIndent,
                    entry.Position.YIndent - textHeight,
                    entry.Position.XIndent + textWidth,
                    entry.Position.YIndent);

                // Create a link annotation that jumps to the corresponding section page.
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect)
                {
                    Action = new GoToAction(sectionPages[i])
                };

                // Add the annotation to the TOC page.
                tocPage.Annotations.Add(link);
            }

            // -----------------------------------------------------------------
            // 4. Save the PDF document.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable Table of Contents saved to '{outputPath}'.");
    }
}
