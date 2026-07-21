using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            StringBuilder sb = new StringBuilder();

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Absorb paragraph structures on the current page
                ParagraphAbsorber absorber = new ParagraphAbsorber();
                absorber.Visit(doc.Pages[pageNum]);

                // Each PageMarkup contains sections and paragraphs
                foreach (PageMarkup pageMarkup in absorber.PageMarkups)
                {
                    foreach (MarkupSection section in pageMarkup.Sections)
                    {
                        foreach (MarkupParagraph paragraph in section.Paragraphs)
                        {
                            // Paragraph.Text includes line breaks within the paragraph
                            sb.AppendLine(paragraph.Text);
                        }
                    }
                }
            }

            // Write the collected text to a .txt file, preserving line breaks
            File.WriteAllText(outputPath, sb.ToString());
        }

        Console.WriteLine($"Paragraph text extracted to '{outputPath}'.");
    }
}