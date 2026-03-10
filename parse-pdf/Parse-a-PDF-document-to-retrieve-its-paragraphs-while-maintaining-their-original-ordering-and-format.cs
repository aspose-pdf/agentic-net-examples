using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // Required for ParagraphAbsorber, PageMarkup, MarkupSection, MarkupParagraph, TextFragment

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
            // Create a ParagraphAbsorber which will detect sections and paragraphs
            ParagraphAbsorber absorber = new ParagraphAbsorber();

            // Perform the search on the whole document (all pages)
            absorber.Visit(doc);

            // The absorber now contains a PageMarkup for each processed page,
            // preserving the original order of pages, sections and paragraphs.
            foreach (PageMarkup pageMarkup in absorber.PageMarkups)
            {
                Console.WriteLine($"--- Page {pageMarkup.Number} ---");

                // Sections are ordered as they appear on the page.
                foreach (MarkupSection section in pageMarkup.Sections)
                {
                    // Paragraphs inside a section are already in reading order.
                    foreach (MarkupParagraph paragraph in section.Paragraphs)
                    {
                        // Retrieve the combined text of the paragraph.
                        string paragraphText = paragraph.Text;

                        // Output the paragraph text while keeping the original ordering.
                        Console.WriteLine(paragraphText);

                        // Optional: iterate over the fragments to access formatting details.
                        foreach (TextFragment fragment in paragraph.Fragments)
                        {
                            // Font information (may be null if not set)
                            var font = fragment.TextState.Font;
                            double fontSize = fragment.TextState.FontSize;
                            Aspose.Pdf.Color color = fragment.TextState.ForegroundColor;

                            // Display formatting for this fragment.
                            Console.WriteLine($"  [Fragment] Font={font?.FontName ?? "default"}, Size={fontSize}, Color={color}");
                        }

                        // Add an empty line to separate paragraphs visually.
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}