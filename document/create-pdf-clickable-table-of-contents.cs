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

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // 1. Add a page that will contain the Table of Contents
            // -------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // -------------------------------------------------
            // 2. Add two sections with headings on separate pages
            // -------------------------------------------------
            // Section 1
            Page sec1Page = doc.Pages.Add();
            TextFragment sec1Heading = new TextFragment("Section 1: Introduction")
            {
                TextState = { FontSize = 24, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Color.Blue }
            };
            sec1Heading.Position = new Position(50, 750);
            sec1Page.Paragraphs.Add(sec1Heading);

            // Section 2
            Page sec2Page = doc.Pages.Add();
            TextFragment sec2Heading = new TextFragment("Section 2: Details")
            {
                TextState = { FontSize = 24, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Color.Blue }
            };
            sec2Heading.Position = new Position(50, 750);
            sec2Page.Paragraphs.Add(sec2Heading);

            // -------------------------------------------------
            // 3. Create TOC entries with clickable links
            // -------------------------------------------------
            // Helper to add a TOC line and its link
            void AddTocEntry(string title, int targetPageIndex)
            {
                // Position for the TOC entry on the TOC page
                double yPos = 700 - (targetPageIndex - 2) * 30; // simple vertical spacing

                // Add visible text for the TOC entry
                TextFragment tocEntry = new TextFragment(title)
                {
                    TextState = { FontSize = 14, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Color.Black }
                };
                tocEntry.Position = new Position(50, yPos);
                tocPage.Paragraphs.Add(tocEntry);

                // Define a rectangle that covers the TOC text (approximate)
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(45, yPos - 5, 300, yPos + 15);

                // Create a link annotation that points to the target heading page
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect);
                // Use an explicit destination (fit the whole page)
                link.Destination = new FitExplicitDestination(doc.Pages[targetPageIndex]);
                link.Color = Color.Blue; // optional visual cue
                tocPage.Annotations.Add(link);
            }

            // Add entries: note that page indexing is 1‑based
            AddTocEntry("1. Introduction", 2); // Section 1 is on page 2
            AddTocEntry("2. Details", 3);      // Section 2 is on page 3

            // -------------------------------------------------
            // 4. Save the document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable TOC saved to '{outputPath}'.");
    }
}