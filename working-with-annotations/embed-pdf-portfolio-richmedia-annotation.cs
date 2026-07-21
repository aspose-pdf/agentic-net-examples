using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";      // PDF to which the annotation will be added
        const string portfolioPdfPath  = "portfolio.pdf";  // PDF portfolio to embed
        const string outputPdfPath     = "output.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine("Input PDF or portfolio PDF not found.");
            return;
        }

        // Load the target PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Use the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation (constructor: RichMediaAnnotation(Page, Rectangle))
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Set activation to occur when the page is opened
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.PageOpen;

            // Optional visual settings
            richMedia.Color = Aspose.Pdf.Color.Transparent;
            richMedia.Border = new Border(richMedia) { Width = 0 };

            // Set the initial view/state to show the first file in the portfolio
            // (ActiveState is a string identifier; "First" selects the first embedded file)
            richMedia.ActiveState = "First";

            // Embed the PDF portfolio as the content stream
            using (FileStream portfolioStream = File.OpenRead(portfolioPdfPath))
            {
                // The first argument is an arbitrary name for the embedded stream
                richMedia.SetContent("portfolio.pdf", portfolioStream);
            }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMediaAnnotation with embedded portfolio saved to '{outputPdfPath}'.");
    }
}