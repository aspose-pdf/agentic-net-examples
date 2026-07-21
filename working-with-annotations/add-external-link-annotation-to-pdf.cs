using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "link_annotation.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Optional visual styling
                Color = Aspose.Pdf.Color.Blue
            };

            // Assign an action that opens an external URL when the annotation is activated
            link.Action = new GoToURIAction("https://www.example.com");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the PDF to the desired file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with external link annotation saved to '{outputPath}'.");
    }
}