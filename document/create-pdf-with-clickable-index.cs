using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class ClickableIndexPdf
{
    static void Main()
    {
        // Output file path
        const string outputPath = "ClickableIndex.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // 1. Create the Index page (first page)
            // -------------------------------------------------
            Page indexPage = doc.Pages.Add();

            // Add a title for the index
            TextFragment indexTitle = new TextFragment("Document Index");
            indexTitle.Position = new Position(50, 750);
            indexTitle.TextState.FontSize = 20;
            indexTitle.TextState.Font = FontRepository.FindFont("Helvetica");
            indexPage.Paragraphs.Add(indexTitle);

            // -------------------------------------------------
            // 2. Create content sections (subsequent pages)
            // -------------------------------------------------
            const int sectionCount = 3;
            Page[] sectionPages = new Page[sectionCount];

            for (int i = 0; i < sectionCount; i++)
            {
                // Add a new page for the section
                Page secPage = doc.Pages.Add();
                sectionPages[i] = secPage;

                // Add a heading to the section page
                string headingText = $"Section {i + 1}";
                TextFragment heading = new TextFragment(headingText);
                heading.Position = new Position(50, 750);
                heading.TextState.FontSize = 18;
                heading.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
                secPage.Paragraphs.Add(heading);

                // Add some placeholder body text
                TextFragment body = new TextFragment($"This is the content of {headingText}.");
                body.Position = new Position(50, 720);
                body.TextState.FontSize = 12;
                body.TextState.Font = FontRepository.FindFont("Helvetica");
                secPage.Paragraphs.Add(body);
            }

            // -------------------------------------------------
            // 3. Add clickable links on the Index page
            // -------------------------------------------------
            for (int i = 0; i < sectionCount; i++)
            {
                // Text for the link entry
                string linkText = $"Go to Section {i + 1}";
                TextFragment linkFragment = new TextFragment(linkText);
                linkFragment.Position = new Position(70, 700 - i * 30);
                linkFragment.TextState.FontSize = 14;
                linkFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                indexPage.Paragraphs.Add(linkFragment);

                // Define the rectangle that covers the link text
                // Approximate rectangle based on text position and length
                double rectLeft   = 70;
                double rectBottom = 700 - i * 30 - 5;
                double rectRight  = rectLeft + linkText.Length * 7; // rough width estimate
                double rectTop    = rectBottom + 15;

                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(rectLeft, rectBottom, rectRight, rectTop);

                // Create a link annotation that points to the target section page
                LinkAnnotation link = new LinkAnnotation(indexPage, linkRect);
                // Use GoToAction to navigate to the target page
                link.Action = new GoToAction(sectionPages[i]);
                link.Color = Aspose.Pdf.Color.Blue; // visual cue for the link
                indexPage.Annotations.Add(link);
            }

            // -------------------------------------------------
            // 4. Save the PDF document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable index saved to '{Path.GetFullPath(outputPath)}'.");
    }
}