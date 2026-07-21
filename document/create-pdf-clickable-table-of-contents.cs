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

        // Create a new PDF document and ensure proper disposal.
        using (Document doc = new Document())
        {
            // -----------------------------------------------------------------
            // 1. Add a page that will contain the Table of Contents (TOC).
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 2. Define some sections – each will be on its own page.
            // -----------------------------------------------------------------
            const int sectionCount = 3;
            Page[] sectionPages = new Page[sectionCount];

            for (int i = 0; i < sectionCount; i++)
            {
                // Add a new page for the section.
                Page secPage = doc.Pages.Add();
                sectionPages[i] = secPage;

                // Add a heading to the section page.
                TextFragment heading = new TextFragment($"Section {i + 1}");
                heading.Position = new Position(50, 750); // top-left position.
                heading.TextState.FontSize = 24;
                heading.TextState.Font = FontRepository.FindFont("Helvetica");
                heading.TextState.ForegroundColor = Color.Black;
                secPage.Paragraphs.Add(heading);
            }

            // -----------------------------------------------------------------
            // 3. Build the TOC entries with clickable links.
            // -----------------------------------------------------------------
            double startX = 50;   // left margin.
            double startY = 750;  // starting vertical position.
            double lineHeight = 30;

            for (int i = 0; i < sectionCount; i++)
            {
                // Text for the TOC entry.
                string entryText = $"Section {i + 1}";
                TextFragment tocEntry = new TextFragment(entryText);
                tocEntry.Position = new Position(startX, startY - i * lineHeight);
                tocEntry.TextState.FontSize = 12;
                tocEntry.TextState.Font = FontRepository.FindFont("Helvetica");
                tocEntry.TextState.ForegroundColor = Color.Blue;
                tocPage.Paragraphs.Add(tocEntry);

                // Approximate rectangle that covers the entry text.
                // Width is estimated; height is based on font size.
                double rectWidth = 200;
                double rectHeight = 12; // approximate height for 12pt font.
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                    startX,
                    startY - i * lineHeight - rectHeight,
                    startX + rectWidth,
                    startY - i * lineHeight
                );

                // Create a link annotation that points to the corresponding section page.
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect);
                // Use an explicit destination (GoToAction) as required by the rule set.
                link.Action = new GoToAction(sectionPages[i]);
                link.Color = Color.Transparent; // make the link invisible.
                tocPage.Annotations.Add(link);
            }

            // -----------------------------------------------------------------
            // 4. Save the PDF document.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable TOC saved to '{outputPath}'.");
    }
}