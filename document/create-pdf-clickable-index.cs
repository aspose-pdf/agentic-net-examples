using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // for link annotations and destination types

class Program
{
    static void Main()
    {
        const string outputPath = "ClickableIndex.pdf";

        // Ensure output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -----------------------------------------------------------------
            // 1. Create the Index page (first page)
            // -----------------------------------------------------------------
            Page indexPage = doc.Pages.Add();

            // Starting Y coordinate for index entries (top of the page)
            double currentY = 800;
            int sectionCount = 3; // Example: three sections

            // -----------------------------------------------------------------
            // 2. Create sections and corresponding index entries
            // -----------------------------------------------------------------
            for (int i = 1; i <= sectionCount; i++)
            {
                // ----- a) Create a section page with a heading -----
                Page sectionPage = doc.Pages.Add();

                // Heading text fragment
                TextFragment heading = new TextFragment($"Section {i}");
                heading.Position = new Position(50, 800);
                heading.TextState.FontSize = 24;
                heading.TextState.FontStyle = FontStyles.Bold;
                sectionPage.Paragraphs.Add(heading);

                // Add some placeholder body text
                TextFragment body = new TextFragment(
                    $"This is the content of section {i}. " +
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
                body.Position = new Position(50, 750);
                body.TextState.FontSize = 12;
                sectionPage.Paragraphs.Add(body);

                // ----- b) Add an entry on the index page -----
                TextFragment indexEntry = new TextFragment($"Section {i}");
                indexEntry.Position = new Position(50, currentY);
                indexEntry.TextState.FontSize = 12;
                indexPage.Paragraphs.Add(indexEntry);

                // Define a rectangle that covers the index entry text.
                // Approximate height of 12pt font ~ 12 points.
                Rectangle linkRect = new Rectangle(
                    50,               // llx (left)
                    currentY - 12,    // lly (lower)
                    300,              // urx (right) – enough width for the text
                    currentY);        // ury (upper)

                // Create a link annotation that points to the top of the section page.
                LinkAnnotation link = new LinkAnnotation(indexPage, linkRect);
                // Use an explicit XYZ destination to jump to the beginning of the section page.
                link.Destination = new XYZExplicitDestination(sectionPage, 0, 800, 1);

                // Add the annotation to the index page.
                indexPage.Annotations.Add(link);

                // Move Y coordinate down for the next entry.
                currentY -= 20;
            }

            // -----------------------------------------------------------------
            // 3. Save the document
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable index saved to '{outputPath}'.");
    }
}
