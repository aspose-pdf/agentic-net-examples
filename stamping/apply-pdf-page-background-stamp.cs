using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDFs
        const string sourcePdf = "source.pdf";   // PDF that provides the background page
        const string targetPdf = "target.pdf";   // PDF that will receive the background stamp
        const string outputPdf = "output.pdf";   // Resulting PDF

        // Verify files exist
        if (!File.Exists(sourcePdf) || !File.Exists(targetPdf))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Load the document that will be stamped (target)
        using (Document targetDoc = new Document(targetPdf))
        {
            // Load the document that contains the background page (source)
            using (Document sourceDoc = new Document(sourcePdf))
            {
                // Choose the page from the source document to use as background.
                // Here we use the first page; change the index as needed (1‑based).
                Page backgroundPage = sourceDoc.Pages[1];

                // Apply the background stamp to each page of the target document.
                for (int i = 1; i <= targetDoc.Pages.Count; i++)
                {
                    Page targetPage = targetDoc.Pages[i];

                    // Create a PdfPageStamp from the selected source page.
                    PdfPageStamp stamp = new PdfPageStamp(backgroundPage)
                    {
                        Background = true,               // Place stamp behind existing content
                        Width = targetPage.Rect.Width,    // Scale to match target page size
                        Height = targetPage.Rect.Height,
                        XIndent = 0,
                        YIndent = 0
                    };

                    // Add the stamp to the current target page.
                    targetPage.AddStamp(stamp);
                }
            }

            // Save the modified document.
            targetDoc.Save(outputPdf);
        }

        Console.WriteLine($"Background stamp applied successfully. Output saved to '{outputPdf}'.");
    }
}