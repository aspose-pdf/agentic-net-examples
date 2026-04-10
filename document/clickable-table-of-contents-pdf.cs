using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableOfContents.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // 1. Create a Table of Contents (TOC) page
            // -------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // Title for TOC
            TextFragment tocTitle = new TextFragment("Table of Contents");
            tocTitle.TextState.Font = FontRepository.FindFont("Helvetica");
            tocTitle.TextState.FontSize = 20;
            tocTitle.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            tocTitle.Position = new Position(50, 750);
            tocPage.Paragraphs.Add(tocTitle);

            // -------------------------------------------------
            // 2. Create content sections and collect their page references
            // -------------------------------------------------
            const int sectionCount = 3;
            Page[] sectionPages = new Page[sectionCount];
            string[] sectionTitles = { "Section 1: Introduction", "Section 2: Details", "Section 3: Conclusion" };

            for (int i = 0; i < sectionCount; i++)
            {
                // Add a new page for the section
                Page secPage = doc.Pages.Add();
                sectionPages[i] = secPage;

                // Add heading text to the section page
                TextFragment heading = new TextFragment(sectionTitles[i]);
                heading.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
                heading.TextState.FontSize = 18;
                heading.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGreen;
                heading.Position = new Position(50, 750);
                secPage.Paragraphs.Add(heading);

                // Add some placeholder body text
                TextFragment body = new TextFragment(
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
                body.TextState.Font = FontRepository.FindFont("Helvetica");
                body.TextState.FontSize = 12;
                body.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                body.Position = new Position(50, 720);
                secPage.Paragraphs.Add(body);
            }

            // -------------------------------------------------
            // 3. Populate TOC with entries linking to sections
            // -------------------------------------------------
            float tocY = 700; // starting Y coordinate for TOC entries
            for (int i = 0; i < sectionCount; i++)
            {
                // Create a text fragment for the TOC entry
                TextFragment entry = new TextFragment(sectionTitles[i]);
                entry.TextState.Font = FontRepository.FindFont("Helvetica");
                entry.TextState.FontSize = 14;
                entry.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                entry.Position = new Position(70, tocY);
                tocPage.Paragraphs.Add(entry);

                // Define a rectangle that roughly covers the text (for the clickable area)
                // Approximate width based on character count; height is 15 points
                float rectWidth = entry.Text.Length * 7; // rough estimate: 7 units per character
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                    entry.Position.XIndent,
                    entry.Position.YIndent - 5,               // lower Y (a bit below baseline)
                    entry.Position.XIndent + rectWidth,
                    entry.Position.YIndent + 12);              // upper Y (above baseline)

                // Create the link annotation (instantiate first, then set properties)
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect);
                link.Color = Aspose.Pdf.Color.Transparent; // invisible border
                link.Action = new GoToAction(sectionPages[i]);
                // Border must be created after the annotation instance exists
                link.Border = new Border(link) { Width = 0 };

                // Add the annotation to the TOC page
                tocPage.Annotations.Add(link);

                // Move down for the next entry
                tocY -= 30;
            }

            // -------------------------------------------------
            // 4. Save the PDF document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable Table of Contents saved to '{outputPath}'.");
    }
}
