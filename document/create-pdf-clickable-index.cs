using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class CreateIndexedPdf
{
    static void Main()
    {
        // Output file path
        const string outputPath = "IndexedDocument.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // 1. Create the Index page (page 1)
            // -------------------------------------------------
            Page indexPage = doc.Pages.Add();

            // Add a title for the index
            TextFragment indexTitle = new TextFragment("Table of Contents")
            {
                // Center the title
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold },
                Position = new Position(0, 800) // X=0 (centered), Y=800
            };
            indexPage.Paragraphs.Add(indexTitle);

            // Define vertical spacing for entries
            double entryY = 750;
            double entryStep = 30;

            // -------------------------------------------------
            // 2. Create Section pages and corresponding index entries
            // -------------------------------------------------
            for (int i = 1; i <= 3; i++)
            {
                // Add a new page for the section
                Page sectionPage = doc.Pages.Add();

                // Add a heading to the section page
                TextFragment heading = new TextFragment($"Section {i}")
                {
                    TextState = { FontSize = 18, FontStyle = FontStyles.Bold },
                    Position = new Position(0, 800) // Centered at top
                };
                sectionPage.Paragraphs.Add(heading);

                // Add some placeholder body text
                TextFragment body = new TextFragment($"This is the content of section {i}.")
                {
                    TextState = { FontSize = 12 },
                    Position = new Position(50, 750)
                };
                sectionPage.Paragraphs.Add(body);

                // -------------------------------------------------
                // 3. Add an entry on the Index page that links to this section
                // -------------------------------------------------
                // Create a visible rectangle for the link annotation
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(50, entryY - 15, 300, entryY + 5);

                // Create the link annotation
                LinkAnnotation link = new LinkAnnotation(indexPage, linkRect)
                {
                    // Use GoToAction to navigate to the target page
                    Action = new GoToAction(sectionPage)
                };

                // Optional: give the link a visible underline style
                link.Color = Aspose.Pdf.Color.Blue;
                link.Border = new Border(link) { Width = 0 }; // No border

                // Add the link annotation to the index page
                indexPage.Annotations.Add(link);

                // Add the textual representation of the entry
                TextFragment entry = new TextFragment($"Section {i} ........................................... {i + 1}")
                {
                    TextState = { FontSize = 12, Underline = true },
                    Position = new Position(50, entryY)
                };
                indexPage.Paragraphs.Add(entry);

                // Move to next entry position
                entryY -= entryStep;
            }

            // -------------------------------------------------
            // 4. Save the document as PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable index saved to '{outputPath}'.");
    }
}
