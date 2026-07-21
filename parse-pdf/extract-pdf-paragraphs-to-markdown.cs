using System;
using System.IO;
using System.Text;
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Absorb paragraphs from the whole document
            ParagraphAbsorber absorber = new ParagraphAbsorber
            {
                // Allow detection of paragraphs that span multiple columns/pages
                IsMulticolumnParagraphsAllowed = true
            };
            absorber.Visit(doc);

            StringBuilder markdown = new StringBuilder();

            // Iterate over each page's markup and extract paragraph text
            foreach (PageMarkup pageMarkup in absorber.PageMarkups)
            {
                foreach (MarkupParagraph paragraph in pageMarkup.Paragraphs)
                {
                    // The Text property retains leading spaces (indentation) from the source PDF
                    string text = paragraph.Text ?? string.Empty;
                    markdown.AppendLine(text);
                }
            }

            // Write the collected text to a markdown file, preserving indentation
            File.WriteAllText(outputMd, markdown.ToString());
        }

        Console.WriteLine($"Markdown file created at '{outputMd}'.");
    }
}