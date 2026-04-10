using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for Heading (inherits TextFragment)

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page to hold the headings
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Heading 1 – Level 1, auto-numbered (e.g., "1. Introduction")
            // -----------------------------------------------------------------
            Heading heading1 = new Heading(1)
            {
                Text = "Introduction",
                IsAutoSequence = true,   // Enable automatic numbering
                StartNumber = 1          // Starting number for this level
            };
            page.Paragraphs.Add(heading1);

            // -----------------------------------------------------------------
            // Heading 2 – Level 2, auto-numbered (e.g., "1.1 Overview")
            // -----------------------------------------------------------------
            Heading heading2 = new Heading(2)
            {
                Text = "Overview",
                IsAutoSequence = true,
                StartNumber = 1
            };
            page.Paragraphs.Add(heading2);

            // -----------------------------------------------------------------
            // Heading 3 – Level 2, manual numbering style (e.g., "A. Details")
            // Since Heading does not expose a direct NumberingStyle property,
            // we simulate a different style by disabling auto-numbering and
            // prefixing the text manually.
            // -----------------------------------------------------------------
            Heading heading3 = new Heading(2)
            {
                Text = "A. Details",   // Manual prefix to represent a different style
                IsAutoSequence = false // Disable automatic numbering
            };
            page.Paragraphs.Add(heading3);

            // -----------------------------------------------------------------
            // Heading 4 – Level 3, auto-numbered (e.g., "1.1.1 Subsection")
            // -----------------------------------------------------------------
            Heading heading4 = new Heading(3)
            {
                Text = "Subsection",
                IsAutoSequence = true,
                StartNumber = 1
            };
            page.Paragraphs.Add(heading4);

            // -----------------------------------------------------------------
            // Heading 5 – Level 1, manual numbering style (e.g., "II. Conclusion")
            // -----------------------------------------------------------------
            Heading heading5 = new Heading(1)
            {
                Text = "II. Conclusion",
                IsAutoSequence = false
            };
            page.Paragraphs.Add(heading5);

            // Save the PDF to disk
            const string outputPath = "StructuredHeadings.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF with structured headings saved to '{outputPath}'.");
        }
    }
}