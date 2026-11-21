using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Insert a new page at the beginning to hold the TOC
            Page tocPage = doc.Pages.Insert(1);
            tocPage.PageInfo.Margin = new MarginInfo(20, 20, 20, 20);

            // Extract all text fragments with formatting information
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(absorber);

            // Collect fragments that look like headings (font size > 14)
            List<TextFragment> headingFragments = new List<TextFragment>();
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.TextState != null && fragment.TextState.FontSize > 14)
                {
                    headingFragments.Add(fragment);
                }
            }

            // Starting vertical position for TOC entries
            double yPos = tocPage.PageInfo.Height - 40;
            const double lineHeight = 20;

            foreach (TextFragment heading in headingFragments)
            {
                // ------------------------------------------------------------
                // 1. Add a Heading object to the TOC page (Aspose.Pdf.Heading)
                // ------------------------------------------------------------
                Heading tocHeading = new Heading(1); // level 1 heading
                tocHeading.IsAutoSequence = true;   // auto‑numbered page reference
                tocHeading.TocPage = tocPage;       // page that holds the TOC
                tocHeading.DestinationPage = heading.Page; // page the heading points to
                // Position the heading on the destination page (top of the page)
                tocHeading.Top = heading.Page.Rect.Height;
                // The visible text of the TOC entry
                tocHeading.Segments.Add(new TextSegment(heading.Text));
                tocPage.Paragraphs.Add(tocHeading);

                // ------------------------------------------------------------
                // 2. Create a clickable link annotation on the TOC page
                // ------------------------------------------------------------
                LinkAnnotation link = new LinkAnnotation(
                    tocPage,
                    new Aspose.Pdf.Rectangle(40, yPos - lineHeight + 5, tocPage.PageInfo.Width - 40, yPos + 5));
                // Destination is the page that contains the heading
                link.Action = new GoToAction(heading.Page);
                // Remove the border of the link annotation
                link.Border = new Border(link) { Width = 0 };
                tocPage.Annotations.Add(link);

                // ------------------------------------------------------------
                // 3. Add an outline (bookmark) entry for the heading
                // ------------------------------------------------------------
                OutlineItemCollection outlineItem = new OutlineItemCollection(doc.Outlines)
                {
                    Title = heading.Text,
                    Destination = new XYZExplicitDestination(
                        heading.Page,
                        0,
                        heading.Page.Rect.Height,
                        1)
                };
                doc.Outlines.Add(outlineItem);

                // ------------------------------------------------------------
                // 4. Move to the next line; add a new TOC page if needed
                // ------------------------------------------------------------
                yPos -= lineHeight;
                if (yPos < 40)
                {
                    tocPage = doc.Pages.Add();
                    tocPage.PageInfo.Margin = new MarginInfo(20, 20, 20, 20);
                    yPos = tocPage.PageInfo.Height - 40;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with TOC saved to '{outputPath}'.");
    }
}
