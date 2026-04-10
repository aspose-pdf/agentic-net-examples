using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string originalPath = "headings.pdf";
        const string sanitizedPath = "headings_sanitized.pdf";

        // ---------- Create PDF with headings ----------
        using (Document doc = new Document())
        {
            // Enable auto‑tagging so heading structure is preserved
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Add a page
            Page page = doc.Pages.Add();

            // Heading level 1
            Heading heading1 = new Heading(1);
            heading1.Text = "Chapter 1: Introduction";
            heading1.IsAutoSequence = true;   // automatic numbering
            heading1.Level = 1;
            page.Paragraphs.Add(heading1);

            // Some regular text
            TextFragment tf1 = new TextFragment("This is the introduction paragraph.");
            page.Paragraphs.Add(tf1);

            // Heading level 2
            Heading heading2 = new Heading(2);
            heading2.Text = "Section 1.1: Background";
            heading2.IsAutoSequence = true;
            heading2.Level = 2;
            page.Paragraphs.Add(heading2);

            // More regular text
            TextFragment tf2 = new TextFragment("Background information goes here.");
            page.Paragraphs.Add(tf2);

            // Save the original PDF
            doc.Save(originalPath);
        }

        // ---------- Load and sanitize while preserving headings ----------
        using (Document doc = new Document(originalPath))
        {
            // Access tagged content (already created by auto‑tagging)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Headings");

            // Example sanitization: remove all annotations
            foreach (Page page in doc.Pages)
            {
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    page.Annotations.Delete(i);
                }
            }

            // Save the sanitized PDF
            doc.Save(sanitizedPath);
        }

        Console.WriteLine($"Created PDF: {originalPath}");
        Console.WriteLine($"Sanitized PDF: {sanitizedPath}");
    }
}