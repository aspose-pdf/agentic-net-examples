using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "source.pdf";      // PDF to which the annotation will be added
        const string portfolioPdfPath = "portfolio.pdf"; // PDF portfolio to embed
        const string outputPdfPath   = "output.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine($"Portfolio PDF not found: {portfolioPdfPath}");
            return;
        }

        // Load the document to be annotated
        using (Document doc = new Document(sourcePdfPath))
        {
            // Create a rectangle for the annotation (coordinates are in points)
            // Here we place it at (100,500) with width 200 and height 250
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 750);

            // Create the RichMediaAnnotation on the first page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect)
            {
                // Optional: give the annotation a title/contents
                Contents = "Embedded PDF Portfolio"
            };

            // Embed the portfolio PDF as the rich media content
            using (FileStream portfolioStream = File.OpenRead(portfolioPdfPath))
            {
                // The first parameter is the name of the embedded stream
                richMedia.SetContent("portfolio.pdf", portfolioStream);
            }

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(richMedia);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdfPath}'.");
    }
}