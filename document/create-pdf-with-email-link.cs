using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "email_link.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the link will appear (lower‑left X,Y, upper‑right X,Y)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a link annotation and set its action to a mailto: URI
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue,                     // optional visual cue
                Action = new GoToURIAction("mailto:example@example.com") // opens default mail client
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with email link saved to '{outputPath}'.");
    }
}