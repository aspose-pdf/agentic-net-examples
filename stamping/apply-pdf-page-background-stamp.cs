using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";          // PDF to be stamped
        const string backgroundPath = "background.pdf"; // PDF containing the background page
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPath}");
            return;
        }
        if (!File.Exists(backgroundPath))
        {
            Console.Error.WriteLine($"Background PDF not found: {backgroundPath}");
            return;
        }

        // Load the document that will receive the stamp
        using (Document targetDoc = new Document(targetPath))
        // Load the document that provides the stamp page
        using (Document bgDoc = new Document(backgroundPath))
        {
            // Select the page from the background document to use as stamp (first page here)
            Page bgPage = bgDoc.Pages[1];

            // Apply the background stamp to each page of the target document
            for (int i = 1; i <= targetDoc.Pages.Count; i++) // 1‑based indexing
            {
                Page targetPage = targetDoc.Pages[i];

                // Create a PdfPageStamp using the selected background page
                PdfPageStamp stamp = new PdfPageStamp(bgPage)
                {
                    Background = true,                         // place behind existing content
                    Width = targetPage.Rect.Width,              // scale to target page width
                    Height = targetPage.Rect.Height,            // scale to target page height
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Add the stamp to the current page
                targetPage.AddStamp(stamp);
            }

            // Save the stamped document
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}