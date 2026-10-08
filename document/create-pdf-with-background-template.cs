using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";   // background page to reuse
        const string outputPath   = "result.pdf";    // final document
        const int    pageCount    = 5;               // number of pages to create

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Load the background PDF (source) and create the target PDF.
        // Both documents are wrapped in using blocks for deterministic disposal.
        using (Document target = new Document())
        using (Document template = new Document(templatePath))
        {
            // The template PDF must have at least one page.
            if (template.Pages.Count == 0)
            {
                Console.Error.WriteLine("Template PDF contains no pages.");
                return;
            }

            // Reference to the first page of the template (1‑based indexing).
            Page templatePage = template.Pages[1];

            // Create the desired number of pages, each based on the template page.
            for (int i = 1; i <= pageCount; i++)
            {
                // Add a copy of the template page to the target document.
                // The Add method clones the page, preserving its background content.
                Page newPage = target.Pages.Add(templatePage);

                // OPTIONAL: add page‑specific content (e.g., page number).
                TextFragment tf = new TextFragment($"Page {i}");
                tf.Position = new Position(50, 750); // coordinates in points
                tf.TextState.FontSize = 14;
                tf.TextState.Font = FontRepository.FindFont("Arial");
                newPage.Paragraphs.Add(tf);
            }

            // Save the assembled PDF. No SaveOptions needed for PDF output.
            target.Save(outputPath);
        }

        Console.WriteLine($"PDF created with background template: {outputPath}");
    }
}