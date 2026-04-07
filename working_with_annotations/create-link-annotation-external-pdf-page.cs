using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the link annotation on the page
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Optional visual styling
                Color = Aspose.Pdf.Color.Blue
            };

            // Set the action to open page 3 of an external PDF file named "external.pdf"
            link.Action = new GoToRemoteAction("external.pdf", 3);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with link annotation created successfully.");
    }
}