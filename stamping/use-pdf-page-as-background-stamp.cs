using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF to be stamped
        const string stampPdf  = "stamp.pdf";   // PDF containing the page used as stamp
        const string outputPdf = "output.pdf";  // Resulting PDF

        // Verify that source files exist
        if (!File.Exists(inputPdf) || !File.Exists(stampPdf))
        {
            Console.Error.WriteLine("One or more input files were not found.");
            return;
        }

        // Load the target document (the one that will receive the background stamp)
        using (Document doc = new Document(inputPdf))
        {
            // Create a PdfPageStamp from page 1 of the stamp PDF.
            // The constructor takes the file name and a 1‑based page index.
            PdfPageStamp pageStamp = new PdfPageStamp(stampPdf, 1);

            // Configure the stamp to appear as a background image.
            pageStamp.Background = true;               // place behind existing content
            pageStamp.Opacity   = 0.5f;                // semi‑transparent
            pageStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page of the target document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageStamp);
            }

            // Save the modified document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}