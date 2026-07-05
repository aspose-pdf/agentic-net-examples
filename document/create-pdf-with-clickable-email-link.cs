using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "email_link.pdf";

        // Document lifecycle must be wrapped in a using block
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the clickable area (coordinates are in points)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a link annotation on the page
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Visual appearance of the link (optional)
                Color = Aspose.Pdf.Color.Blue
            };

            // Border requires the parent annotation in its constructor
            link.Border = new Border(link) { Width = 1 };

            // Use GoToURIAction for a mailto link (cannot assign string directly)
            link.Action = new GoToURIAction("mailto:example@example.com");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with email link saved to '{outputPath}'.");
    }
}