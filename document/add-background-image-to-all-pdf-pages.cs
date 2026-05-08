using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath    = "input.pdf";      // PDF to which the background will be added
        const string templatePdfPath = "background_template.pdf"; // PDF containing the background image (first page)
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
            // Load the document that will receive the background
            using (Document targetDoc = new Document(inputPdfPath))
            // Load the template containing the background image (assumed on page 1)
            using (Document templateDoc = new Document(templatePdfPath))
            {
                // Get the template page that holds the background image
                Page templatePage = templateDoc.Pages[1]; // 1‑based indexing

                // Iterate over all pages of the target document
                for (int i = 1; i <= targetDoc.Pages.Count; i++)
                {
                    Page page = targetDoc.Pages[i];

                    // Create a stamp from the template page
                    PdfPageStamp stamp = new PdfPageStamp(templatePage)
                    {
                        // Place the stamp behind existing page content
                        Background = true
                    };

                    // Apply the stamp to the current page
                    page.AddStamp(stamp);
                }

                // Save the modified document
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