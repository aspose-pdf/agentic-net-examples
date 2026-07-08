using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "NumberedHeadings.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // ---------- Level 1 Heading ----------
            // Create a heading of level 1
            Heading heading1 = new Heading(1)
            {
                Text = "Chapter 1: Introduction",
                // Enable automatic numbering
                IsAutoSequence = true,
                // Use Arabic decimal numbers for numbering
                Style = NumberingStyle.NumeralsArabic,
                // Set visual appearance (optional)
                TextState = { FontSize = 20, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(heading1);

            // Add some body text after the heading
            TextFragment body1 = new TextFragment("This is the introductory text for chapter 1.")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(body1);

            // ---------- Level 2 Heading ----------
            // Create a heading of level 2 (subsection)
            Heading heading2 = new Heading(2)
            {
                Text = "Section 1.1: Background",
                IsAutoSequence = true,
                Style = NumberingStyle.NumeralsArabic,
                TextState = { FontSize = 16, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(heading2);

            // Add body text for the subsection
            TextFragment body2 = new TextFragment("Background information goes here.")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(body2);

            // ---------- Level 3 Heading ----------
            // Create a heading of level 3 (sub‑subsection)
            Heading heading3 = new Heading(3)
            {
                Text = "Subsection 1.1.1: Details",
                IsAutoSequence = true,
                Style = NumberingStyle.NumeralsArabic,
                TextState = { FontSize = 14, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(heading3);

            // Add body text for the sub‑subsection
            TextFragment body3 = new TextFragment("Detailed discussion of the topic.")
            {
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };
            page.Paragraphs.Add(body3);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hierarchical numbered headings saved to '{outputPath}'.");
    }
}