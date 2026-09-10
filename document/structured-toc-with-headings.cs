using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "StructuredTOC.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to hold the content
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Create Heading objects with different numbering styles
            // -----------------------------------------------------------------
            // Heading level 1 – Arabic numbering (1., 2., 3.)
            Heading heading1 = new Heading(1);
            heading1.Text = "Chapter 1: Introduction";
            heading1.IsAutoSequence = true;
            // The NumberingStyle property is not available in the current SDK version.
            // If a later version provides it, uncomment the line below and use the full enum name.
            // heading1.NumberingStyle = NumberingStyle.NumeralsArabic;
            heading1.TextState.Font = FontRepository.FindFont("Helvetica");
            heading1.TextState.FontSize = 20;
            heading1.TextState.ForegroundColor = Color.Black;
            page.Paragraphs.Add(heading1);

            // Heading level 2 – Uppercase Roman numerals (I., II., III.)
            Heading heading2 = new Heading(2);
            heading2.Text = "Section 1.1: Overview";
            heading2.IsAutoSequence = true;
            // heading2.NumberingStyle = NumberingStyle.NumeralsRomanUppercase;
            heading2.TextState.Font = FontRepository.FindFont("Helvetica");
            heading2.TextState.FontSize = 16;
            heading2.TextState.ForegroundColor = Color.DarkGray;
            page.Paragraphs.Add(heading2);

            // Heading level 3 – Lowercase letters (a., b., c.)
            Heading heading3 = new Heading(3);
            heading3.Text = "Subsection 1.1.1: Details";
            heading3.IsAutoSequence = true;
            // heading3.NumberingStyle = NumberingStyle.LettersLowercase;
            heading3.TextState.Font = FontRepository.FindFont("Helvetica");
            heading3.TextState.FontSize = 14;
            heading3.TextState.ForegroundColor = Color.Gray;
            page.Paragraphs.Add(heading3);

            // -----------------------------------------------------------------
            // Build a structured Table of Contents (TOC) using tagged content
            // -----------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Document with Structured TOC");

            // Root element of the logical structure
            StructureElement root = tagged.RootElement;

            // Create a TOC element
            TOCElement toc = tagged.CreateTOCElement();
            // Title can be set directly as a string or via a TextFragment (both are valid)
            toc.Title = "Table of Contents";
            root.AppendChild(toc);

            // Add TOC entries (TOCI elements) that reference the headings
            // First entry for Heading 1
            TOCIElement tocItem1 = tagged.CreateTOCIElement();
            tocItem1.ActualText = "1. Chapter 1: Introduction .................................... 1";
            toc.AppendChild(tocItem1);

            // Second entry for Heading 2
            TOCIElement tocItem2 = tagged.CreateTOCIElement();
            tocItem2.ActualText = "I. Section 1.1: Overview ...................................... 1";
            toc.AppendChild(tocItem2);

            // Third entry for Heading 3
            TOCIElement tocItem3 = tagged.CreateTOCIElement();
            tocItem3.ActualText = "a. Subsection 1.1.1: Details .................................. 1";
            toc.AppendChild(tocItem3);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with structured TOC saved to '{outputPath}'.");
    }
}
