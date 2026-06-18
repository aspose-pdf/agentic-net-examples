using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, PdfPageStamp)
using Aspose.Pdf.Facades;      // Optional, not required for PdfPageStamp but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPdfPath    = "input.pdf";      // PDF to which the background will be added
        const string templatePdfPath = "background_template.pdf"; // PDF containing a single page with the desired background image
        const string outputPdfPath   = "output_with_background.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        try
        {
            // Load the target document and the template (both wrapped in using for deterministic disposal)
            using (Document targetDoc = new Document(inputPdfPath))
            using (Document templateDoc = new Document(templatePdfPath))
            {
                // Assume the template contains exactly one page that represents the background.
                // If it has more pages, you can select the appropriate one (e.g., templateDoc.Pages[1]).
                Page backgroundPage = templateDoc.Pages[1];

                // Iterate over every page of the target document (1‑based indexing)
                for (int i = 1; i <= targetDoc.Pages.Count; i++)
                {
                    Page currentPage = targetDoc.Pages[i];

                    // Create a stamp that uses the background page as its source.
                    PdfPageStamp stamp = new PdfPageStamp(backgroundPage)
                    {
                        // Place the stamp behind existing page content.
                        Background = true,

                        // Optional: scale the stamp to fit the page.
                        // Width and Height default to the original page size; adjust if needed.
                        // Example: stamp.Width = currentPage.MediaBox.Width;
                        //          stamp.Height = currentPage.MediaBox.Height;
                    };

                    // Apply the stamp to the current page.
                    currentPage.AddStamp(stamp);
                }

                // Save the modified document.
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Background image applied to all pages. Saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}