using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "link_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add two pages (page numbers are 1‑based)
            doc.Pages.Add(); // page 1
            doc.Pages.Add(); // page 2

            // Define the rectangle area for the link annotation on page 1
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a link annotation that jumps to page 2
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], rect);
            // Use GoToAction with a FitExplicitDestination to target page 2
            link.Action = new GoToAction(doc.Pages[2]);

            // Add the annotation to the first page
            doc.Pages[1].Annotations.Add(link);

            // ----- Tagged PDF part -----
            // Obtain the tagged‑content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Create a LinkElement (structure element with role /Link)
            LinkElement linkElement = tagged.CreateLinkElement();

            // Associate a LocalHyperlink that points to page 2
            LocalHyperlink localLink = new LocalHyperlink
            {
                TargetPageNumber = 2 // internal page number (1‑based)
            };
            linkElement.Hyperlink = localLink;

            // Bind the structure element to the visual annotation
            linkElement.Tag(link);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}