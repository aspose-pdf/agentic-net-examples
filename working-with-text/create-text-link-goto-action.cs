using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // existing PDF or create new
        const string outputPath = "output.pdf";

        // Ensure the input file exists; if not, create a simple PDF with two pages
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                // Page 1
                Page page1 = tempDoc.Pages.Add();
                TextFragment tf1 = new TextFragment("Click here to go to Page 2");
                tf1.Position = new Position(100, 700);
                page1.Paragraphs.Add(tf1);

                // Page 2
                Page page2 = tempDoc.Pages.Add();
                page2.Paragraphs.Add(new TextFragment("You are now on Page 2."));

                tempDoc.Save(inputPath);
            }
        }

        // Open the document, add a link that navigates to page 2
        using (Document doc = new Document(inputPath))
        {
            // Ensure there are at least two pages
            if (doc.Pages.Count < 2)
                doc.Pages.Add();

            // Add a visible text fragment on the first page (optional if already present)
            Page firstPage = doc.Pages[1];
            TextFragment linkText = new TextFragment("Go to Page 2");
            linkText.Position = new Position(100, 750);
            linkText.TextState.FontSize = 14;
            linkText.TextState.Font = FontRepository.FindFont("Helvetica");
            linkText.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            firstPage.Paragraphs.Add(linkText);

            // Define a rectangle that covers the text fragment (approximate)
            // Rectangle(left, bottom, right, top) – coordinates are in points
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 730, 250, 760);

            // Create the LinkAnnotation first
            LinkAnnotation link = new LinkAnnotation(firstPage, linkRect);
            // Optional visual styling
            link.Color = Aspose.Pdf.Color.Transparent;
            link.Border = new Border(link) { Width = 0 };
            // Assign a GoToAction that jumps to page 2
            link.Action = new GoToAction(doc.Pages[2]);

            // Add the annotation to the page
            firstPage.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with text link saved to '{outputPath}'.");
    }
}
