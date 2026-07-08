using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputMd = "output.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Absorb paragraphs from the whole document
            ParagraphAbsorber absorber = new ParagraphAbsorber();
            absorber.Visit(doc);

            // Iterate over each page markup (preserves page order)
            for (int i = 0; i < absorber.PageMarkups.Count; i++)
            {
                PageMarkup pageMarkup = absorber.PageMarkups[i];
                // Corresponding page (1‑based indexing)
                Page page = doc.Pages[i + 1];
                double pageLeft = page.Rect.LLX; // left edge of the page

                foreach (MarkupParagraph paragraph in pageMarkup.Paragraphs)
                {
                    if (paragraph.Fragments.Count == 0)
                        continue;

                    // Use the X position of the first fragment as the indent reference
                    TextFragment firstFragment = paragraph.Fragments[0];
                    // Position.X is not available; use XIndent instead
                    double indentPoints = firstFragment.Position.XIndent - pageLeft;

                    // Convert points to a rough number of spaces (5 points ≈ one space)
                    int spaceCount = Math.Max(0, (int)Math.Round(indentPoints / 5.0));

                    // Preserve original text but prepend calculated spaces
                    string originalText = paragraph.Text ?? string.Empty;
                    paragraph.Text = new string(' ', spaceCount) + originalText.TrimStart();
                }
            }

            // Save the modified document as Markdown, passing explicit save options
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();
            doc.Save(outputMd, mdOptions);
        }

        Console.WriteLine($"Markdown file created: {outputMd}");
    }
}
