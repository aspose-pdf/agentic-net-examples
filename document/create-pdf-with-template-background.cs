using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePath = "template.pdf";   // PDF page to be used as background
        const string outputPath   = "output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Load the template PDF (the page that will serve as background)
        using (Document templateDoc = new Document(templatePath))
        {
            // Ensure the template has at least one page
            if (templateDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Template PDF contains no pages.");
                return;
            }

            // The first page of the template will be stamped onto each new page
            Page templatePage = templateDoc.Pages[1];

            // Create the output document
            using (Document outputDoc = new Document())
            {
                // Example: create 3 pages that use the template as background
                for (int i = 1; i <= 3; i++)
                {
                    // Add a new blank page to the output document
                    Page newPage = outputDoc.Pages.Add();

                    // Create a stamp based on the template page
                    PdfPageStamp stamp = new PdfPageStamp(templatePage)
                    {
                        // Render the stamp as background so page content appears on top
                        Background = true,
                        // Optional: adjust position or scaling if needed
                        // XIndent = 0,
                        // YIndent = 0,
                        // Zoom = 1.0f
                    };

                    // Apply the stamp to the newly created page
                    newPage.AddStamp(stamp);

                    // Add some sample content to the page (e.g., a text fragment)
                    TextFragment tf = new TextFragment($"Page {i} – content over template background")
                    {
                        // Position the text near the top of the page
                        Position = new Position(100, 750),
                        // Simple styling
                        TextState = { FontSize = 14, Font = FontRepository.FindFont("Helvetica") }
                    };
                    newPage.Paragraphs.Add(tf);
                }

                // Save the resulting PDF
                outputDoc.Save(outputPath);
                Console.WriteLine($"PDF created successfully: {outputPath}");
            }
        }
    }
}