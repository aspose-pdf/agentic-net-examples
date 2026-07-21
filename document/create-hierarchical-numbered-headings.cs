using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "numbered_headings.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // ------------------------------
            // Heading Level 1 (e.g., Chapter)
            // ------------------------------
            // The constructor takes the heading level (1 for top level)
            Heading heading1 = new Heading(1);
            heading1.Text = "Chapter 1";
            heading1.IsAutoSequence = true;                     // Enable automatic numbering
            heading1.Style = NumberingStyle.NumeralsArabic;    // Decimal numbering (Arabic numerals)
            heading1.Level = 1;                                 // Explicitly set the level (optional, matches constructor)
            heading1.StartNumber = 1;                           // Starting number for this heading level
            page.Paragraphs.Add(heading1);

            // Add a blank line for visual separation
            page.Paragraphs.Add(new TextFragment("\n"));

            // ------------------------------
            // Heading Level 2 (e.g., Section)
            // ------------------------------
            Heading heading2a = new Heading(2);
            heading2a.Text = "Section 1.1";
            heading2a.IsAutoSequence = true;
            heading2a.Style = NumberingStyle.NumeralsArabic;
            heading2a.Level = 2;
            heading2a.StartNumber = 1;
            page.Paragraphs.Add(heading2a);

            page.Paragraphs.Add(new TextFragment("\n"));

            // Another Level 2 heading to demonstrate hierarchical increment
            Heading heading2b = new Heading(2);
            heading2b.Text = "Section 1.2";
            heading2b.IsAutoSequence = true;
            heading2b.Style = NumberingStyle.NumeralsArabic;
            heading2b.Level = 2;
            // StartNumber can be omitted; auto‑sequence will continue from the previous heading
            page.Paragraphs.Add(heading2b);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hierarchical numbered headings saved to '{outputPath}'.");
    }
}