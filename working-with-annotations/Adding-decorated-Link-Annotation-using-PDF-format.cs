using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for GoToAction and GoToURIAction

class Program
{
    static void Main()
    {
        const string outputPath = "decorated_link_annotation.pdf";

        // Create a new PDF document with a single blank page
        using (Document doc = new Document())
        {
            // Add a blank page (size A4)
            Page page = doc.Pages.Add();

            // Define the rectangle area for the link annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, linkRect);

            // Set a visible border (width = 2 points)
            link.Border = new Border(link) { Width = 2 };

            // Set the border color
            link.Color = Aspose.Pdf.Color.Blue;

            // Set the highlight mode when the mouse is over the link
            link.Highlighting = HighlightingMode.Invert;

            // Optional: set a tooltip that appears on hover
            link.Contents = "Visit Aspose.Pdf website";

            // Example 1: External URL link using GoToURIAction
            link.Action = new GoToURIAction("https://www.aspose.com/pdf");

            // Example 2: Internal link to page 2 (uncomment to use)
            // Ensure the document has at least two pages before using this
            // Page targetPage = doc.Pages.Add(); // adds a second page
            // link.Action = new GoToAction(targetPage);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with decorated link annotation saved to '{outputPath}'.");
    }
}
