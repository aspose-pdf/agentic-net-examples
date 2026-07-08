using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalPath  = "original.pdf";
        const string sanitizedPath = "sanitized.pdf";

        // -------------------------------------------------
        // Step 1: Create a PDF with heading structure
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Enable auto‑tagging so that headings are recognized in the structure tree
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Create Heading 1
            Heading heading1 = new Heading(1)          // level 1 heading
            {
                Text = "Chapter 1: Introduction",
                IsAutoSequence = true                 // enable automatic numbering
            };
            page.Paragraphs.Add(heading1);

            // Add some normal paragraph text
            TextFragment para1 = new TextFragment("This is the introductory paragraph of chapter 1.")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(para1);

            // Create Heading 2
            Heading heading2 = new Heading(2)          // level 2 heading
            {
                Text = "Section 1.1: Background",
                IsAutoSequence = true
            };
            page.Paragraphs.Add(heading2);

            // Add another paragraph
            TextFragment para2 = new TextFragment("Background information goes here.")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(para2);

            // Save the original PDF
            doc.Save(originalPath);
        }

        // -------------------------------------------------
        // Step 2: Load the PDF and produce a sanitized copy
        // -------------------------------------------------
        using (Document doc = new Document(originalPath))
        {
            // Example sanitization: remove all annotations (if any)
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing; clear all
                while (page.Annotations.Count > 0)
                {
                    page.Annotations.Delete(1);
                }
            }

            // Preserve the heading structure – no changes needed because headings are part of the content
            // Save the sanitized PDF
            doc.Save(sanitizedPath);
        }

        Console.WriteLine($"Original PDF saved to '{originalPath}'.");
        Console.WriteLine($"Sanitized PDF saved to '{sanitizedPath}'.");
    }
}