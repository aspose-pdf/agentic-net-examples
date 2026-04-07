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

            // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation that opens an external URL
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue,                     // Visual appearance of the link border
                Action = new GoToURIAction("https://www.example.com") // Open the URL when clicked
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}