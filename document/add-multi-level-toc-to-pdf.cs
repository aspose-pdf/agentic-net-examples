using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Insert a new page at the beginning to hold the Table of Contents
            Page tocPage = doc.Pages.Insert(1);

            // Prepare positioning for TOC entries
            double marginLeft = 50;
            double marginTop = 50;
            double lineHeight = 20;
            double currentY = tocPage.PageInfo.Height - marginTop;

            // Title for the TOC
            TextFragment title = new TextFragment("Table of Contents");
            title.TextState.Font = FontRepository.FindFont("Arial");
            title.TextState.FontSize = 24;
            title.Position = new Position(marginLeft, currentY);
            tocPage.Paragraphs.Add(title);
            currentY -= lineHeight * 2; // extra space after title

            // Iterate over the original pages (now starting at index 2)
            for (int pageIdx = 2; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Extract text fragments from the page using TextFragmentAbsorber
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                page.Accept(absorber);

                // Examine each fragment to find headings based on font size
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    double fontSize = fragment.TextState.FontSize;
                    int headingLevel;

                    if (fontSize >= 24) headingLevel = 1;
                    else if (fontSize >= 20) headingLevel = 2;
                    else if (fontSize >= 16) headingLevel = 3;
                    else continue; // not a heading

                    // Build the TOC entry text with indentation
                    string indent = new string(' ', (headingLevel - 1) * 4);
                    string entryText = $"{indent}{fragment.Text.Trim()} ........ {pageIdx - 1}";

                    // Create a TextFragment for the TOC entry
                    TextFragment tocEntry = new TextFragment(entryText);
                    tocEntry.TextState.Font = FontRepository.FindFont("Arial");
                    tocEntry.TextState.FontSize = 12;
                    tocEntry.Position = new Position(marginLeft, currentY);

                    tocPage.Paragraphs.Add(tocEntry);
                    currentY -= lineHeight;

                    // If we run out of space on the TOC page, add a new TOC page
                    if (currentY < marginTop)
                    {
                        tocPage = doc.Pages.Insert(doc.Pages.Count + 1);
                        currentY = tocPage.PageInfo.Height - marginTop;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Table of Contents saved to '{outputPath}'.");
    }
}
