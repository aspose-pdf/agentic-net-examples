using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "email_link.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define the clickable area (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation that opens the default mail client
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Visual cue (optional)
                Color = Aspose.Pdf.Color.Blue,
                // Use GoToURIAction with a mailto: URL (cannot assign string directly)
                Action = new GoToURIAction("mailto:example@example.com")
            };

            // Attach the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with email link saved to '{outputPath}'.");
    }
}