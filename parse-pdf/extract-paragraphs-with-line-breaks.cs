using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // StringBuilder to accumulate extracted paragraph text with line breaks
            StringBuilder sb = new StringBuilder();

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // ParagraphAbsorber extracts paragraph structures from the page
                ParagraphAbsorber absorber = new ParagraphAbsorber();
                absorber.Visit(page);

                // Each PageMarkup represents the layout of one page
                foreach (PageMarkup pageMarkup in absorber.PageMarkups)
                {
                    // Sections contain paragraphs
                    foreach (MarkupSection section in pageMarkup.Sections)
                    {
                        foreach (MarkupParagraph paragraph in section.Paragraphs)
                        {
                            // Preserve line breaks by iterating over Lines
                            foreach (var lineFragments in paragraph.Lines)
                            {
                                // Concatenate text fragments of the line
                                foreach (TextFragment fragment in lineFragments)
                                {
                                    sb.Append(fragment.Text);
                                }
                                sb.AppendLine(); // line break
                            }

                            // Add an extra blank line between paragraphs
                            sb.AppendLine();
                        }
                    }
                }
            }

            // Write the accumulated text to a .txt file
            File.WriteAllText(outputTxtPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Paragraph text extracted to '{outputTxtPath}'.");
        }
    }
}