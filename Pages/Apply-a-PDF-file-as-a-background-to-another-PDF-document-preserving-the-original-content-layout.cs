using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for PdfPageStamp

class Program
{
    static void Main()
    {
        const string targetPath   = "target.pdf";    // PDF whose content will stay on top
        const string backgroundPath = "background.pdf"; // PDF to be used as background
        const string outputPath   = "combined.pdf";

        if (!File.Exists(targetPath) || !File.Exists(backgroundPath))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load the target document (the one that keeps its original content)
        using (Document targetDoc = new Document(targetPath))
        // Load the background document (the one whose pages become the background)
        using (Document bgDoc = new Document(backgroundPath))
        {
            // Ensure both documents have the same number of pages.
            // If they differ, the shorter length is used.
            int pageCount = Math.Min(targetDoc.Pages.Count, bgDoc.Pages.Count);

            for (int i = 1; i <= pageCount; i++)
            {
                // Get the corresponding pages
                Page targetPage = targetDoc.Pages[i];
                Page bgPage     = bgDoc.Pages[i];

                // Create a stamp from the background page
                PdfPageStamp stamp = new PdfPageStamp(bgPage)
                {
                    // Place the stamp behind the existing content
                    Background = true,
                    // Optional: match the size of the target page
                    // (if background page size differs, you can adjust Width/Height)
                    // Width  = targetPage.Rect.Width,
                    // Height = targetPage.Rect.Height
                };

                // Apply the stamp to the target page
                targetPage.AddStamp(stamp);
            }

            // Save the resulting document
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Background applied successfully. Output saved to '{outputPath}'.");
    }
}