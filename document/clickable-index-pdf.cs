using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class ClickableIndexExample
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "ClickableIndex.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // 1. Create the Index page (first page)
            // -------------------------------------------------
            Page indexPage = doc.Pages.Add();

            // Title for the index
            TextFragment indexTitle = new TextFragment("Document Index")
            {
                // Use a larger font size for the title
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold, ForegroundColor = Color.Black }
            };
            // Position the title near the top of the page
            indexTitle.Position = new Position(50, 750);
            indexPage.Paragraphs.Add(indexTitle);

            // -------------------------------------------------
            // 2. Create content sections on separate pages
            // -------------------------------------------------
            // We'll create three sections as an example
            const int sectionCount = 3;
            Page[] sectionPages = new Page[sectionCount];
            for (int i = 0; i < sectionCount; i++)
            {
                // Add a new page for the section
                Page secPage = doc.Pages.Add();

                // Store reference for later linking
                sectionPages[i] = secPage;

                // Section heading
                string headingText = $"Section {i + 1}";
                TextFragment heading = new TextFragment(headingText)
                {
                    TextState = { FontSize = 18, FontStyle = FontStyles.Bold, ForegroundColor = Color.DarkBlue }
                };
                heading.Position = new Position(50, 750);
                secPage.Paragraphs.Add(heading);

                // Sample body text
                TextFragment body = new TextFragment($"This is the content of {headingText}.")
                {
                    TextState = { FontSize = 12, ForegroundColor = Color.Black }
                };
                body.Position = new Position(50, 720);
                secPage.Paragraphs.Add(body);
            }

            // -------------------------------------------------
            // 3. Populate the Index with entries and link annotations
            // -------------------------------------------------
            // Starting Y coordinate for the first entry
            double entryY = 700;
            for (int i = 0; i < sectionCount; i++)
            {
                // Text for the index entry
                string entryText = $"Go to Section {i + 1}";
                TextFragment entry = new TextFragment(entryText)
                {
                    TextState = { FontSize = 14, ForegroundColor = Color.Blue }
                };
                entry.Position = new Position(70, entryY);
                indexPage.Paragraphs.Add(entry);

                // Approximate rectangle covering the entry text
                // (left, bottom, right, top) – values chosen to enclose the text
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(70, entryY - 5, 300, entryY + 15);

                // Create a link annotation that navigates to the corresponding section page
                LinkAnnotation link = new LinkAnnotation(indexPage, linkRect)
                {
                    // Use GoToAction to jump to the target page
                    Action = new GoToAction(sectionPages[i]),
                    // Optional visual cue (blue underline)
                    Color = Color.Blue
                };

                // Add the annotation to the page
                indexPage.Annotations.Add(link);

                // Move down for the next entry
                entryY -= 30;
            }

            // -------------------------------------------------
            // 4. Save the PDF document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable index saved to '{Path.GetFullPath(outputPath)}'.");
    }
}