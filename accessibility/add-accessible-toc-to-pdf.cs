using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates it if missing)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Find all heading elements (H1‑H6) in the document
            var headings = root.FindElements<HeaderElement>(true);

            // -------------------------------------------------
            // 1. Build the logical TOC structure (a Div element)
            // -------------------------------------------------
            DivElement tocDiv = tagged.CreateDivElement();
            tocDiv.AlternativeText = "Table of Contents";

            // Title of the TOC
            ParagraphElement tocTitle = tagged.CreateParagraphElement();
            tocTitle.SetText("Table of Contents");
            tocDiv.AppendChild(tocTitle);

            // Add an entry for each heading
            foreach (HeaderElement heading in headings)
            {
                // Create a paragraph element for the TOC entry
                ParagraphElement entry = tagged.CreateParagraphElement();
                // Use the heading's visible text
                string headingText = heading.ActualText ?? string.Empty;
                entry.SetText(headingText);
                tocDiv.AppendChild(entry);
            }

            // Insert the TOC structure as the first child of the root
            // (so it appears before existing content in the logical order)
            root.InsertChild(tocDiv, 0); // corrected argument order

            // -------------------------------------------------
            // 2. Add a visual TOC page at the beginning of the PDF
            // -------------------------------------------------
            // Insert a new blank page at position 1 (first page)
            Page tocPage = doc.Pages.Insert(1);

            // Simple layout variables
            double marginLeft = 50;
            double marginTop  = 750;
            double lineHeight = 20;

            // Title on the visual page
            TextFragment titleFragment = new TextFragment("Table of Contents")
            {
                Position = new Position(marginLeft, marginTop),
                TextState = { FontSize = 18, FontStyle = FontStyles.Bold }
            };
            tocPage.Paragraphs.Add(titleFragment);

            // List each heading below the title
            double currentY = marginTop - lineHeight * 2;
            foreach (HeaderElement heading in headings)
            {
                string headingText = heading.ActualText ?? string.Empty;
                TextFragment line = new TextFragment(headingText)
                {
                    Position = new Position(marginLeft + 20, currentY),
                    TextState = { FontSize = 12 }
                };
                tocPage.Paragraphs.Add(line);
                currentY -= lineHeight;
                // Prevent overflow; start a new column if needed (simple check)
                if (currentY < 50)
                {
                    currentY = marginTop - lineHeight * 2;
                    marginLeft += 250; // move to next column
                }
            }

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with accessible TOC saved to '{outputPath}'.");
    }
}
