using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";      // source PDF
        const string outputPath  = "output.pdf";     // result PDF
        const string externalPdf = "external.pdf";   // PDF to open
        const int    externalPage = 3;               // page number in external PDF (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have a page to place the annotation on
            Page page = doc.Pages[1];

            // Define the clickable rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Blue,                     // visual border color
                Contents = $"Open page {externalPage} of external PDF" // tooltip text
            };

            // Assign a remote go‑to action that opens the specified page of another PDF
            link.Action = new GoToRemoteAction(externalPdf, externalPage);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified document (using rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with link annotation: '{outputPath}'.");
    }
}