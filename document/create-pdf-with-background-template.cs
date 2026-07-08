using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, PdfPageStamp, etc.)

class Program
{
    static void Main()
    {
        // Paths for the template (background) PDF and the resulting PDF.
        const string templatePath = "template.pdf";
        const string outputPath   = "output.pdf";

        // Number of pages to generate in the new document.
        const int pageCount = 5;

        // Verify that the template file exists.
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Load the template PDF that will serve as the background.
        using (Document templateDoc = new Document(templatePath))
        {
            // Get the first page of the template (1‑based indexing).
            Page templatePage = templateDoc.Pages[1];

            // Create a new empty PDF document.
            using (Document newDoc = new Document())
            {
                // Add the desired number of pages, stamping each with the template background.
                for (int i = 1; i <= pageCount; i++)
                {
                    // Add a blank page to the new document.
                    Page page = newDoc.Pages.Add();

                    // Create a stamp that uses the template page as its content.
                    PdfPageStamp stamp = new PdfPageStamp(templatePage)
                    {
                        // Place the stamp behind the page content so it acts as a background.
                        Background = true
                    };

                    // Apply the stamp to the current page.
                    page.AddStamp(stamp);
                }

                // Save the resulting PDF.
                newDoc.Save(outputPath);
                Console.WriteLine($"PDF created with background template: {outputPath}");
            }
        }
    }
}