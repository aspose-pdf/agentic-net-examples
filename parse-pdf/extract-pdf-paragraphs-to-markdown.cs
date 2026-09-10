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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a ParagraphAbsorber to extract paragraph structures from the document
            ParagraphAbsorber absorber = new ParagraphAbsorber();

            // Perform the absorption on the whole document
            absorber.Visit(doc);

            // Open a StreamWriter for the markdown output file
            using (StreamWriter writer = new StreamWriter(outputMd, false, System.Text.Encoding.UTF8))
            {
                // Iterate over each page's markup information
                foreach (PageMarkup pageMarkup in absorber.PageMarkups)
                {
                    // Iterate over each paragraph found on the page
                    foreach (MarkupParagraph paragraph in pageMarkup.Paragraphs)
                    {
                        // The Text property contains the paragraph text including leading spaces,
                        // which preserves the original indentation.
                        string text = paragraph.Text;

                        // Write the paragraph to the markdown file
                        writer.WriteLine(text);
                        // Add an empty line to separate markdown paragraphs
                        writer.WriteLine();
                    }
                }
            }

            Console.WriteLine($"Markdown file saved to '{outputMd}'.");
        }
    }
}