using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "output.pdf";         // result PDF
        const string portfolioPath  = "portfolio.pdf";      // PDF portfolio to embed

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio PDF not found: {portfolioPath}");
            return;
        }

        // Load the document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the RichMedia annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMedia annotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // OPTIONAL: set activation event if needed (commented out because enum values may vary)
            // richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.EveryTime;

            // Embed the PDF portfolio as the rich media content
            using (FileStream portfolioStream = File.OpenRead(portfolioPath))
            {
                // The first argument is a name for the embedded file
                richMedia.SetContent("Portfolio", portfolioStream);
            }

            // Set the initial view to show the first file in the portfolio.
            // For RichMediaAnnotation the ActiveState can be used to specify the initial view.
            // Here we set it to the name of the embedded file.
            richMedia.ActiveState = "Portfolio";

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMedia annotation added and saved to '{outputPdfPath}'.");
    }
}