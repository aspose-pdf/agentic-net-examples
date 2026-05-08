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

        // Document lifecycle must be managed with using
        using (Document doc = new Document())
        {
            // -------------------------
            // Create the index page
            // -------------------------
            Page indexPage = doc.Pages.Add();

            // Title for the index
            TextFragment title = new TextFragment("Index");
            title.TextState.FontSize = 20;
            title.Position = new Position(50, 800);
            indexPage.Paragraphs.Add(title);

            // -------------------------
            // Create content sections
            // -------------------------
            string[] sections = { "Introduction", "Chapter 1", "Conclusion" };
            Page[] sectionPages = new Page[sections.Length];

            for (int i = 0; i < sections.Length; i++)
            {
                // Add a new page for each section
                Page secPage = doc.Pages.Add();
                sectionPages[i] = secPage;

                // Section heading
                TextFragment heading = new TextFragment(sections[i]);
                heading.TextState.FontSize = 18;
                heading.Position = new Position(50, 800);
                secPage.Paragraphs.Add(heading);
            }

            // -------------------------
            // Add index entries with clickable links
            // -------------------------
            for (int i = 0; i < sections.Length; i++)
            {
                // Position each entry vertically
                double y = 750 - i * 30;

                // Visible text for the entry
                TextFragment entry = new TextFragment($"{i + 1}. {sections[i]}");
                entry.Position = new Position(50, y);
                indexPage.Paragraphs.Add(entry);

                // Define a rectangle that covers the entry text
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, y - 5, 300, y + 15);

                // Create a link annotation that navigates to the target section page
                LinkAnnotation link = new LinkAnnotation(indexPage, rect);
                // Use GoToAction for internal navigation (rule: no-destination-class-use-explicit-destination)
                link.Action = new GoToAction(sectionPages[i]);
                // Optional visual cue
                link.Color = Aspose.Pdf.Color.Blue;

                indexPage.Annotations.Add(link);
            }

            // -------------------------
            // Save the PDF (lifecycle rule)
            // -------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable index saved to '{outputPath}'.");
    }
}