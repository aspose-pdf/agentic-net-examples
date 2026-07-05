using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "linked_output.pdf";

        // Create a new PDF document with two pages (self‑contained example)
        using (Document doc = new Document())
        {
            // Add two blank pages
            doc.Pages.Add(); // Page 1 – source page for the link
            doc.Pages.Add(); // Page 2 – target page for the named destination

            // ------------------------------------------------------------
            // Define a named destination on page 2 (fit the whole page)
            // ------------------------------------------------------------
            Page targetPage = doc.Pages[2];
            // Create an explicit destination that fits the page
            FitExplicitDestination fitDest = new FitExplicitDestination(targetPage);
            // Register the named destination in the document
            doc.NamedDestinations.Add("MyDestination", fitDest);

            // ------------------------------------------------------------
            // Create a link annotation on page 1 that jumps to the named destination
            // ------------------------------------------------------------
            Page sourcePage = doc.Pages[1];
            // Define the rectangle area for the clickable link (left, bottom, right, top)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            // Instantiate the link annotation
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect);
            // Set the action to go to the previously defined named destination
            link.Action = new GoToAction(doc, "MyDestination");
            // Optional visual styling for the link annotation
            link.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page
            sourcePage.Annotations.Add(link);

            // ------------------------------------------------------------
            // Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }
    }
}
