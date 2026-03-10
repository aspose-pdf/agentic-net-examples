using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source XSL‑FO file and the resulting PDF file
        const string xslFoPath   = "source.xslfo";
        const string outputPdf   = "output_with_link.pdf";

        // Verify that the XSL‑FO file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO file and convert it to a PDF document
        XslFoLoadOptions loadOptions = new XslFoLoadOptions(xslFoPath);
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Choose the page where the link annotation will be placed (first page)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle area for the link annotation (coordinates are in points)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 250, 540);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Set a visible border color (optional)
                Color = Aspose.Pdf.Color.Blue,
                // Optional tooltip text shown when the mouse hovers over the link
                Contents = "Visit Example.com"
            };

            // Define the action – navigate to an external URL when the annotation is activated
            link.Action = new GoToURIAction("https://www.example.com");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPdf}'.");
    }
}
