using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputMd  = "output.md";          // markdown result

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            StringBuilder sb = new StringBuilder();

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Absorb paragraphs from the current page
                ParagraphAbsorber absorber = new ParagraphAbsorber();
                absorber.Visit(page);

                // Each PageMarkup contains the detected paragraphs
                foreach (MarkupParagraph paragraph in absorber.PageMarkups[0].Paragraphs)
                {
                    // The Text property retains leading spaces (indentation)
                    sb.AppendLine(paragraph.Text);
                }
            }

            // Write the collected paragraphs to a markdown file.
            // Leading spaces are preserved, giving the desired indentation.
            File.WriteAllText(outputMd, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Markdown file created: {outputMd}");
    }
}