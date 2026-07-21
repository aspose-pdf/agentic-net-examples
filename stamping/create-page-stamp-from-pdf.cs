using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "template.pdf";   // PDF containing the page to be used as a stamp
        const string sourcePdfPath   = "source.pdf";    // PDF to which the stamp will be applied
        const string outputPdfPath   = "output.pdf";

        // Verify files exist
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the template PDF and obtain the page that will serve as the stamp
        using (Document templateDoc = new Document(templatePdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page stampSourcePage = templateDoc.Pages[1];

            // Create a PdfPageStamp from the selected page
            PdfPageStamp pageStamp = new PdfPageStamp(stampSourcePage)
            {
                // Example configuration – stamp as background with 50% opacity
                Background = true,
                Opacity    = 0.5f,
                // Optional positioning – center the stamp on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Load the document that will receive the stamp
            using (Document targetDoc = new Document(sourcePdfPath))
            {
                // Apply the stamp to every page of the target document
                foreach (Page page in targetDoc.Pages)
                {
                    page.AddStamp(pageStamp);
                }

                // Save the resulting PDF
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}