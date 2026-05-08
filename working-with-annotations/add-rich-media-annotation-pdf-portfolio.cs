using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";      // source PDF
        const string portfolioPdfPath  = "portfolio.pdf";  // PDF portfolio to embed
        const string outputPdfPath     = "output.pdf";     // result PDF

        if (!File.Exists(inputPdfPath) || !File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the source PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a RichMediaAnnotation on the first page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect);

            // Set the activation event so the media opens when the page is displayed
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.PageOpen;

            // Embed the PDF portfolio as the rich media content
            using (FileStream portfolioStream = File.OpenRead(portfolioPdfPath))
            {
                richMedia.SetContent(Path.GetFileName(portfolioPdfPath), portfolioStream);
            }

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(richMedia);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMediaAnnotation added. Output saved to '{outputPdfPath}'.");
    }
}