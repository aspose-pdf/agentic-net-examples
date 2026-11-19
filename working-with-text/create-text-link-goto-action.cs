using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "linked_output.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add the target page (page 2) that the link will navigate to
            Page targetPage = doc.Pages.Add();
            // Optional: add some content to the target page
            TextFragment targetText = new TextFragment("You have arrived at page 2.");
            targetPage.Paragraphs.Add(targetText);

            // Add the source page (page 1) where the link will be placed
            Page sourcePage = doc.Pages.Add();

            // Create a TextFragment that will act as a visual hyperlink
            TextFragment linkFragment = new TextFragment("Go to page 2");
            linkFragment.TextState.FontSize = 14;
            linkFragment.TextState.Underline = true;
            linkFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            // Position the text (optional – helps define the clickable area)
            linkFragment.Position = new Position(50, 750);
            sourcePage.Paragraphs.Add(linkFragment);

            // Create a LinkAnnotation that covers the same area as the text fragment
            // The rectangle coordinates are left, bottom, right, top.
            // Here we approximate the rectangle based on the known font size.
            var linkRect = new Aspose.Pdf.Rectangle(50, 735, 200, 755);
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect);
            // Assign a GoTo action that points to page number 2 (1‑based indexing)
            link.Action = new GoToAction(2);
            // Add the annotation to the page
            sourcePage.Annotations.Add(link);

            // Save the PDF; default format is PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}
