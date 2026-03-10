using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a ParagraphAbsorber to extract paragraph structures
            ParagraphAbsorber absorber = new ParagraphAbsorber();

            // Perform the absorption on the whole document
            absorber.Visit(doc);

            // Iterate over each page's markup to retrieve paragraphs
            int pageIndex = 0;
            foreach (PageMarkup pageMarkup in absorber.PageMarkups)
            {
                pageIndex++;
                Console.WriteLine($"--- Page {pageIndex} ---");
                foreach (MarkupSection section in pageMarkup.Sections)
                {
                    foreach (MarkupParagraph paragraph in section.Paragraphs)
                    {
                        // Output the paragraph text
                        Console.WriteLine(paragraph.Text);
                    }
                }
            }
        }
    }
}
