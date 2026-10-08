using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "ClickableIndex.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -----------------------------------------------------------------
            // 1. Create the Index page (first page)
            // -----------------------------------------------------------------
            Page indexPage = doc.Pages.Add();

            // Title for the index
            TextFragment indexTitle = new TextFragment("Document Index");
            indexTitle.TextState.FontSize = 20;
            indexTitle.Position = new Position(50, 800);
            indexPage.Paragraphs.Add(indexTitle);

            // -----------------------------------------------------------------
            // 2. Define sections that will be linked from the index
            // -----------------------------------------------------------------
            string[] sections = { "Section 1: Introduction", "Section 2: Details", "Section 3: Conclusion" };

            // Vertical position for the first link entry
            double linkY = 750;

            for (int i = 0; i < sections.Length; i++)
            {
                // -------------------------------------------------------------
                // a) Create a new page for the current section
                // -------------------------------------------------------------
                Page sectionPage = doc.Pages.Add();

                // Add a heading to the section page
                TextFragment heading = new TextFragment(sections[i]);
                heading.TextState.FontSize = 16;
                heading.Position = new Position(50, 800);
                sectionPage.Paragraphs.Add(heading);

                // -------------------------------------------------------------
                // b) Add a visible link entry on the index page
                // -------------------------------------------------------------
                // Text that will appear on the index page
                TextFragment linkText = new TextFragment(sections[i]);
                linkText.Position = new Position(55, linkY);
                linkText.TextState.FontSize = 12;
                linkText.TextState.Underline = true;
                linkText.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                indexPage.Paragraphs.Add(linkText);

                // Rectangle that defines the clickable area (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(50, linkY - 15, 300, linkY + 5);

                // Create a link annotation on the index page
                LinkAnnotation link = new LinkAnnotation(indexPage, linkRect);

                // Set the action to go to the corresponding section page
                link.Action = new GoToAction(sectionPage.Number);

                // Add the annotation to the index page
                indexPage.Annotations.Add(link);

                // Move down for the next link entry
                linkY -= 30;
            }

            // -----------------------------------------------------------------
            // 3. Save the document
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable index saved to '{outputPath}'.");
    }
}