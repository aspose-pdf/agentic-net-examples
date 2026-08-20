using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "email_link.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the clickable area will appear (llx, lly, urx, ury)
            // Adjust coordinates as needed; here we place it near the top-left corner
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create the link annotation on the page
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            // Optional visual styling
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Set the action to open the default mail client with a pre‑filled address
            link.Action = new GoToURIAction("mailto:someone@example.com");

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with email link saved to '{outputPath}'.");
    }
}