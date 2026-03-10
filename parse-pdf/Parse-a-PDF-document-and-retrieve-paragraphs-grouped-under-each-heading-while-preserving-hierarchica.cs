using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a ParagraphAbsorber to detect sections (headings) and paragraphs
            ParagraphAbsorber absorber = new ParagraphAbsorber();

            // Perform the search on the whole document
            absorber.Visit(doc);

            // Iterate over each page's markup information
            for (int pageIdx = 0; pageIdx < absorber.PageMarkups.Count; pageIdx++)
            {
                PageMarkup pageMarkup = absorber.PageMarkups[pageIdx];
                Console.WriteLine($"Page {pageIdx + 1}:");

                int sectionIdx = 1;
                // Each MarkupSection corresponds to a logical section (often a heading)
                foreach (MarkupSection section in pageMarkup.Sections)
                {
                    Console.WriteLine($"  Section {sectionIdx}:");

                    int paragraphIdx = 1;
                    // Paragraphs belonging to the current section
                    foreach (MarkupParagraph paragraph in section.Paragraphs)
                    {
                        // Concatenate all text fragments of the paragraph
                        string paragraphText = string.Empty;
                        foreach (TextFragment fragment in paragraph.Fragments)
                        {
                            paragraphText += fragment.Text;
                        }

                        Console.WriteLine($"    Paragraph {paragraphIdx}: {paragraphText}");
                        paragraphIdx++;
                    }

                    sectionIdx++;
                }
            }
        }
    }
}