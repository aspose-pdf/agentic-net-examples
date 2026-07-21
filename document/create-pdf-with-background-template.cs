using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";   // PDF page to be used as background
        const string outputPath   = "output.pdf";    // Resulting PDF
        const int    pageCount    = 5;               // Number of pages to create

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Load the template PDF (contains the background page)
        using (Document templateDoc = new Document(templatePath))
        {
            // Create a new empty PDF document
            using (Document outputDoc = new Document())
            {
                // Use the first page of the template as the background source
                Page backgroundPage = templateDoc.Pages[1];

                for (int i = 1; i <= pageCount; i++)
                {
                    // Add a new blank page to the output document
                    Page newPage = outputDoc.Pages.Add();

                    // Ensure the new page has the same size and margins as the background page
                    newPage.PageInfo = backgroundPage.PageInfo;

                    // Create a stamp that uses the background page
                    PdfPageStamp stamp = new PdfPageStamp(backgroundPage)
                    {
                        Background = true   // place stamp behind any future content
                    };

                    // Apply the stamp to the newly created page
                    newPage.AddStamp(stamp);
                }

                // Save the resulting PDF
                outputDoc.Save(outputPath);
                Console.WriteLine($"PDF created with background template: {outputPath}");
            }
        }
    }
}