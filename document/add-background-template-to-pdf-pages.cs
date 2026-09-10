using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for PdfPageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";      // PDF to which background will be added
        const string backgroundPdfPath = "background.pdf"; // PDF containing the background template (single page)
        const string outputPdfPath     = "output_with_background.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundPdfPath))
        {
            Console.Error.WriteLine($"Background template PDF not found: {backgroundPdfPath}");
            return;
        }

        try
        {
            // Load the target document (the one to be stamped)
            using (Document targetDoc = new Document(inputPdfPath))
            // Load the template document that contains the background page
            using (Document templateDoc = new Document(backgroundPdfPath))
            {
                // Assume the template has at least one page; use the first page as the stamp source
                Page templatePage = templateDoc.Pages[1];

                // Iterate over all pages of the target document (1‑based indexing)
                for (int i = 1; i <= targetDoc.Pages.Count; i++)
                {
                    Page currentPage = targetDoc.Pages[i];

                    // Create a stamp that uses the template page
                    PdfPageStamp stamp = new PdfPageStamp(templatePage)
                    {
                        // Place the stamp behind existing content
                        Background = true,

                        // Optional: scale the stamp to fit the page size
                        // Width and Height default to the template page size; adjust if needed
                        // Example: fit to the current page dimensions
                        Width  = currentPage.MediaBox.Width,
                        Height = currentPage.MediaBox.Height,

                        // Optional: set opacity (0.0 – fully transparent, 1.0 – fully opaque)
                        Opacity = 1.0f
                    };

                    // Apply the stamp to the current page
                    currentPage.AddStamp(stamp);
                }

                // Save the modified document
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Background image added to all pages. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}