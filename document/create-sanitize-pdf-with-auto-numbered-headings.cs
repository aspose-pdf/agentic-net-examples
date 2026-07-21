using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string originalPath  = "original.pdf";
        const string sanitizedPath = "sanitized.pdf";

        // -------------------------------------------------
        // 1. Create a PDF with headings and some body text
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Heading 1 – auto‑numbered
            Heading h1 = new Heading(1)
            {
                Text            = "Chapter 1: Introduction",
                Level           = 1,
                IsAutoSequence = true   // enable automatic numbering
            };
            page.Paragraphs.Add(h1);

            // Some paragraph under heading 1
            TextFragment para1 = new TextFragment("This is the introductory paragraph of chapter 1.")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(para1);

            // Heading 2 – auto‑numbered
            Heading h2 = new Heading(1)
            {
                Text            = "Section 1.1: Background",
                Level           = 2,
                IsAutoSequence = true
            };
            page.Paragraphs.Add(h2);

            // Paragraph under heading 2
            TextFragment para2 = new TextFragment("Background information goes here.")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(para2);

            // Add a second page with another heading
            Page page2 = doc.Pages.Add();

            Heading h3 = new Heading(1)
            {
                Text            = "Chapter 2: Methodology",
                Level           = 1,
                IsAutoSequence = true
            };
            page2.Paragraphs.Add(h3);

            TextFragment para3 = new TextFragment("Details about the methodology.")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            page2.Paragraphs.Add(para3);

            // Save the original document
            doc.Save(originalPath);
        }

        // -------------------------------------------------
        // 2. Load the PDF and sanitize it while keeping headings
        // -------------------------------------------------
        using (Document doc = new Document(originalPath))
        {
            // Enable auto‑tagging to preserve structural information
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // OPTIONAL: configure heading detection thresholds if needed
            // AutoTaggingSettings.Default.HeadingLevels = new HeadingLevels();

            // Remove all annotations (if any)
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing
                while (page.Annotations.Count > 0)
                {
                    page.Annotations.Delete(page.Annotations.Count);
                }
            }

            // Remove document metadata (title, author, etc.)
            doc.RemoveMetadata();

            // Flatten form fields (if any) to plain content
            doc.Flatten();

            // Save the sanitized PDF
            doc.Save(sanitizedPath);
        }

        Console.WriteLine($"Original PDF saved to '{originalPath}'.");
        Console.WriteLine($"Sanitized PDF saved to '{sanitizedPath}'.");
    }
}