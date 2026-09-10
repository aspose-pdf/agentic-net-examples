using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "HeadingsSanitized.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define a font to be used for headings and body text
            Aspose.Pdf.Text.Font headingFont = FontRepository.FindFont("Helvetica-Bold");
            Aspose.Pdf.Text.Font bodyFont    = FontRepository.FindFont("Helvetica");

            // Create three headings with numbering and some body text after each heading
            for (int i = 1; i <= 3; i++)
            {
                // Heading text (e.g., "1. Introduction")
                TextFragment heading = new TextFragment($"{i}. Heading {i}");
                heading.TextState.Font = headingFont;
                heading.TextState.FontSize = 16;          // larger font for heading
                heading.TextState.ForegroundColor = Color.Black;
                heading.Margin = new MarginInfo { Top = 10, Bottom = 5 };
                page.Paragraphs.Add(heading);

                // Body paragraph under the heading
                TextFragment body = new TextFragment($"This is the body text for heading {i}. It provides details related to the section.");
                body.TextState.Font = bodyFont;
                body.TextState.FontSize = 12;
                body.TextState.ForegroundColor = Color.Black;
                body.Margin = new MarginInfo { Bottom = 15 };
                page.Paragraphs.Add(body);
            }

            // Enable auto‑tagging so that headings are recognized and structured in the PDF
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            // (Optional) configure heading levels if custom mapping is required
            // AutoTaggingSettings.Default.HeadingLevels = new HeadingLevels();

            // Sanitize the document while preserving its structure
            doc.RemoveMetadata();          // strip metadata
            doc.RemovePdfUaCompliance();   // remove PDF/UA compliance flags
            doc.Optimize();                // linearize for web delivery

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with headings created and sanitized: {Path.GetFullPath(outputPath)}");
    }
}