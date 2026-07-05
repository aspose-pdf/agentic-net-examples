using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // PDF that provides the page to be used as a stamp template
        const string templatePdfPath = "template.pdf";
        // PDF that will receive the stamp
        const string sourcePdfPath   = "input.pdf";
        // Resulting PDF after stamping
        const string outputPdfPath   = "output.pdf";

        // Verify that both input files exist
        if (!File.Exists(templatePdfPath) || !File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine("One or more input files were not found.");
            return;
        }

        // Load the document containing the template page
        using (Document templateDoc = new Document(templatePdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; select the first page as the stamp source
            Page templatePage = templateDoc.Pages[1];

            // Load the document that will be stamped
            using (Document targetDoc = new Document(sourcePdfPath))
            {
                // Create a PdfPageStamp from the template page
                PdfPageStamp pageStamp = new PdfPageStamp(templatePage);

                // Configure stamp appearance (optional)
                pageStamp.Background = false;               // place stamp on top of page content
                pageStamp.Opacity    = 0.5;                  // semi‑transparent
                pageStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageStamp.VerticalAlignment   = VerticalAlignment.Center;

                // Apply the stamp to each page of the target document
                foreach (Page page in targetDoc.Pages)
                {
                    page.AddStamp(pageStamp);
                }

                // Save the stamped document
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}