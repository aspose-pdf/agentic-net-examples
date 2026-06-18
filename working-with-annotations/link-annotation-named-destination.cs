using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with two pages
        using (Document createDoc = new Document())
        {
            // First page – will contain the link annotation
            Page firstPage = createDoc.Pages.Add();
            TextFragment tf1 = new TextFragment("Page 1 – Click the rectangle to go to Page 2");
            firstPage.Paragraphs.Add(tf1);

            // Second page – target of the link
            Page secondPage = createDoc.Pages.Add();
            TextFragment tf2 = new TextFragment("Page 2 – Destination");
            secondPage.Paragraphs.Add(tf2);

            // Save the temporary PDF
            createDoc.Save("input.pdf");
        }

        // Step 2: Open the PDF, add a named destination and a link annotation
        using (Document doc = new Document("input.pdf"))
        {
            // Define a named destination on the second page (Fit view)
            Page targetPage = doc.Pages[2];
            FitExplicitDestination explicitDest = new FitExplicitDestination(targetPage);
            doc.NamedDestinations.Add("MyDestination", explicitDest);

            // Create a link annotation on the first page that points to the named destination
            Page sourcePage = doc.Pages[1];
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect);
            link.Action = new GoToAction(doc, "MyDestination");
            sourcePage.Annotations.Add(link);

            // Save the final PDF
            doc.Save("output.pdf");
        }
    }
}