using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class ClickableIndexPdf
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // 1. Create the Index page (first page of the PDF)
            // -------------------------------------------------
            Page indexPage = doc.Pages.Add();

            // Add a title for the index
            TextFragment indexTitle = new TextFragment("Index");
            indexTitle.TextState.FontSize = 20;
            indexTitle.Position = new Position(50, 800);
            indexPage.Paragraphs.Add(indexTitle);

            // -------------------------------------------------
            // 2. Create several sections (each on its own page)
            // -------------------------------------------------
            int sectionCount = 3; // example: three sections
            for (int i = 1; i <= sectionCount; i++)
            {
                // Add a new page for the section
                Page sectionPage = doc.Pages.Add();

                // Section heading
                TextFragment heading = new TextFragment($"Section {i}");
                heading.TextState.FontSize = 18;
                heading.Position = new Position(50, 800);
                sectionPage.Paragraphs.Add(heading);

                // Dummy content for the section
                TextFragment content = new TextFragment($"This is the content of section {i}.");
                content.Position = new Position(50, 750);
                content.TextState.FontSize = 12;
                sectionPage.Paragraphs.Add(content);
            }

            // -------------------------------------------------
            // 3. Add clickable entries on the Index page
            // -------------------------------------------------
            for (int i = 1; i <= sectionCount; i++)
            {
                // Visual text for the index entry
                TextFragment entry = new TextFragment($"Section {i}");
                entry.Position = new Position(50, 760 - (i - 1) * 30);
                entry.TextState.FontSize = 12;
                entry.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                indexPage.Paragraphs.Add(entry);

                // Define a rectangle that will act as the clickable area
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                    50,                     // left
                    750 - (i - 1) * 30,    // bottom
                    200,                    // right
                    770 - (i - 1) * 30);   // top

                // Create a link annotation and set its action to navigate to the target page
                LinkAnnotation link = new LinkAnnotation(indexPage, linkRect);
                // GoToAction navigates to a specific page within the same document
                link.Action = new GoToAction(doc.Pages[i + 1]); // i+1 because page 1 is the index
                link.Color = Aspose.Pdf.Color.Blue; // optional visual cue
                indexPage.Annotations.Add(link);
            }

            // -------------------------------------------------
            // 4. Save the PDF
            // -------------------------------------------------
            doc.Save("ClickableIndex.pdf");
        }

        Console.WriteLine("PDF with clickable index created successfully.");
    }
}