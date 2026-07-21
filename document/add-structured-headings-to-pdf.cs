using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextFragment‑derived types like Heading

class Program
{
    static void Main()
    {
        const string outputPath = "StructuredTOC.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the headings
            Page page = doc.Pages.Add();

            // ---------- Heading level 1 ----------
            // The constructor argument defines the heading level (1‑6)
            Heading heading1 = new Heading(1);
            heading1.Text = "Chapter 1: Introduction";
            // Enable automatic numbering (e.g., 1, 2, 3 …)
            heading1.IsAutoSequence = true;
            // Add the heading to the page content
            page.Paragraphs.Add(heading1);

            // ---------- Heading level 2 ----------
            Heading heading2 = new Heading(2);
            heading2.Text = "Section 1.1: Background";
            heading2.IsAutoSequence = true;
            page.Paragraphs.Add(heading2);

            // ---------- Heading level 3 ----------
            Heading heading3 = new Heading(3);
            heading3.Text = "Subsection 1.1.1: History";
            heading3.IsAutoSequence = true;
            page.Paragraphs.Add(heading3);

            // ---------- Additional headings with different levels ----------
            Heading heading4 = new Heading(2);
            heading4.Text = "Section 1.2: Scope";
            heading4.IsAutoSequence = true;
            page.Paragraphs.Add(heading4);

            Heading heading5 = new Heading(1);
            heading5.Text = "Chapter 2: Methodology";
            heading5.IsAutoSequence = true;
            page.Paragraphs.Add(heading5);

            // Save the PDF – no SaveOptions needed because the target format is PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with structured headings saved to '{outputPath}'.");
    }
}