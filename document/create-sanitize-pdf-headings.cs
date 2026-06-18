using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalPath   = "original.pdf";
        const string sanitizedPath  = "sanitized.pdf";

        // -------------------------------------------------
        // 1. Create a PDF with headings and some regular text
        // -------------------------------------------------
        using (Document srcDoc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page srcPage = srcDoc.Pages.Add();

            // Heading level 1 – will be auto‑numbered
            Heading heading1 = new Heading(1);
            heading1.Text = "Chapter 1";
            heading1.IsAutoSequence = true; // enable automatic numbering
            srcPage.Paragraphs.Add(heading1);

            // Regular paragraph (will be removed during sanitization)
            TextFragment regular = new TextFragment("This is some regular content that will be stripped.");
            srcPage.Paragraphs.Add(regular);

            // Heading level 2 – sub‑section
            Heading heading2 = new Heading(2);
            heading2.Text = "Section 1.1";
            heading2.IsAutoSequence = true;
            srcPage.Paragraphs.Add(heading2);

            // Another regular paragraph
            srcPage.Paragraphs.Add(new TextFragment("More non‑heading text."));

            // Second top‑level heading
            Heading heading3 = new Heading(1);
            heading3.Text = "Chapter 2";
            heading3.IsAutoSequence = true;
            srcPage.Paragraphs.Add(heading3);

            // Save the original document
            srcDoc.Save(originalPath);
        }

        // -------------------------------------------------
        // 2. Sanitize the PDF – keep only headings, preserve numbering
        // -------------------------------------------------
        using (Document srcDoc = new Document(originalPath))
        using (Document sanitizedDoc = new Document())
        {
            // Create a new page for the sanitized output
            Page sanitizedPage = sanitizedDoc.Pages.Add();

            // Iterate over all pages (1‑based) of the source document
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                Page srcPage = srcDoc.Pages[i];

                // Iterate over all paragraph objects on the page
                foreach (var paragraph in srcPage.Paragraphs)
                {
                    // Keep only objects of type Heading
                    if (paragraph is Heading srcHeading)
                    {
                        // Clone the heading to retain its auto‑numbering state
                        Heading clonedHeading = (Heading)srcHeading.Clone();
                        sanitizedPage.Paragraphs.Add(clonedHeading);
                    }
                }
            }

            // Save the sanitized PDF
            sanitizedDoc.Save(sanitizedPath);
        }

        Console.WriteLine($"Original PDF saved to '{originalPath}'.");
        Console.WriteLine($"Sanitized PDF saved to '{sanitizedPath}'.");
    }
}