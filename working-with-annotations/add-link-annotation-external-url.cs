using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Path to the output PDF file
        const string outputPath = "LinkAnnotation.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle area for the link annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Set a visible border color (optional)
                Color = Aspose.Pdf.Color.Blue,
                // Set the action to open an external URL
                Action = new GoToURIAction("https://www.example.com")
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}