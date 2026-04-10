using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";   // PDF that contains the template page
        const string targetPdf = "target.pdf";   // PDF to which the stamp will be applied
        const string outputPdf = "output.pdf";

        if (!File.Exists(sourcePdf) || !File.Exists(targetPdf))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document srcDoc = new Document(sourcePdf))
        using (Document tgtDoc = new Document(targetPdf))
        {
            // Select the page from the source document that will act as the stamp template
            // Aspose.Pdf uses 1‑based page indexing
            Page templatePage = srcDoc.Pages[1];

            // Create a PdfPageStamp using the selected template page
            PdfPageStamp pageStamp = new PdfPageStamp(templatePage);

            // Optional: configure stamp appearance and placement
            pageStamp.Background = false;                     // stamp on top of content
            pageStamp.Opacity = 0.8;                          // semi‑transparent
            pageStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageStamp.VerticalAlignment = VerticalAlignment.Center;

            // Apply the stamp to a page in the target document (example: first page)
            Page targetPage = tgtDoc.Pages[1];
            targetPage.AddStamp(pageStamp);

            // Save the modified target document
            tgtDoc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}