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
            // ---------- Create Index Page ----------
            Page indexPage = doc.Pages.Add();
            // Add a title for the index
            TextFragment indexTitle = new TextFragment("Table of Contents")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold }
            };
            indexPage.Paragraphs.Add(indexTitle);
            indexPage.Paragraphs.Add(new TextFragment("\n")); // spacer

            // Number of sections to create
            int sectionCount = 3;

            // Store the Y position for each index entry
            double currentY = 100; // start from top (PDF coordinate origin is bottom-left)

            // Loop to create sections and corresponding index entries
            for (int i = 1; i <= sectionCount; i++)
            {
                // ---------- Create Section Page ----------
                Page sectionPage = doc.Pages.Add();
                // Add a heading for the section
                TextFragment heading = new TextFragment($"Section {i}")
                {
                    TextState = { FontSize = 18, FontStyle = FontStyles.Bold }
                };
                sectionPage.Paragraphs.Add(heading);
                sectionPage.Paragraphs.Add(new TextFragment("\nThis is the content of section " + i + "."));

                // ---------- Add Index Entry ----------
                // Text for the index entry
                string entryText = $"Section {i}";
                TextFragment entryFragment = new TextFragment(entryText);
                indexPage.Paragraphs.Add(entryFragment);
                indexPage.Paragraphs.Add(new TextFragment("\n"));

                // Approximate rectangle covering the index entry text
                // (left, bottom, right, top) in points
                double left = 50;
                double bottom = currentY;
                double right = 300;
                double top = currentY + 15; // height of text line

                // Create a link annotation that points to the section page
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(left, bottom, right, top);
                LinkAnnotation link = new LinkAnnotation(indexPage, linkRect)
                {
                    // Use a visible border color for demonstration (optional)
                    Color = Aspose.Pdf.Color.Blue,
                    // Action navigates to the target page
                    Action = new GoToAction(sectionPage)
                };
                // Add the annotation to the index page
                indexPage.Annotations.Add(link);

                // Update Y position for next entry
                currentY -= 20; // move down for next entry
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable index saved to '{outputPath}'.");
    }
}