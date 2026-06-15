using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // ----- First level heading (e.g., Chapter 1) -----
            Heading heading1 = new Heading(1) // level 1 heading
            {
                Text = "Chapter 1: Introduction",
                IsAutoSequence = true,          // enable automatic numbering
                StartNumber = 1,                // start numbering from 1
                Level = 1,                      // hierarchical level
                Style = NumberingStyle.NumeralsArabic // decimal numbering (1, 2, 3, ...)
            };
            page.Paragraphs.Add(heading1);

            // Add some space after the heading
            page.Paragraphs.Add(new TextFragment("\n"));

            // ----- Second level heading (e.g., Section 1.1) -----
            Heading heading2 = new Heading(2) // level 2 heading
            {
                Text = "Section 1.1: Overview",
                IsAutoSequence = true,          // enable automatic numbering
                StartNumber = 1,                // start numbering from 1 within this level
                Level = 2,                      // hierarchical level
                Style = NumberingStyle.NumeralsArabic // decimal hierarchical numbering (1.1, 1.2, ...)
            };
            page.Paragraphs.Add(heading2);

            // Add more content so the page label can be observed
            page.Paragraphs.Add(new TextFragment("\nThis is some sample content under the headings."));

            // Save the PDF
            string outputPath = "HeadingNumbered.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}