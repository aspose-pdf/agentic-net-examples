using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "LinkAnnotationDemo.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle area for the link annotation
            // Fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create the link annotation on the page
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Optional visual appearance
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Visit Aspose.Pdf website"
            };

            // Set the action to open an external URL.
            // Do NOT assign a string to Hyperlink – use GoToURIAction instead.
            link.Action = new GoToURIAction("https://www.aspose.com/pdf");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}