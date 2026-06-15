using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Path to the PDF portfolio that will be embedded as rich media
        const string portfolioPath = "portfolio.pdf";
        // Output PDF that will contain the RichMediaAnnotation
        const string outputPath = "richmedia_output.pdf";

        // Ensure the source portfolio file exists
        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio file not found: {portfolioPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page where the annotation will be placed
            Page page = doc.Pages.Add();

            // Define the rectangle area for the annotation (coordinates are in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation on the page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Activate the annotation when the page is opened
                ActivateOn = RichMediaAnnotation.ActivationEvent.PageOpen
            };

            // Embed the PDF portfolio as the rich‑media content.
            // The first parameter is the MIME type; the second is the content stream.
            using (FileStream portfolioStream = File.OpenRead(portfolioPath))
            {
                richMedia.SetContent("application/pdf", portfolioStream);
            }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPath}'.");
    }
}