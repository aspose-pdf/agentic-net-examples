using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "link_annotation.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation and set its appearance
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue,                     // Visual cue (optional)
                Action = new GoToURIAction("https://www.example.com") // Open external URL
            };

            // Attach the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with external link saved to '{outputPath}'.");
    }
}