using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Absorb paragraph structures from the whole document
            ParagraphAbsorber absorber = new ParagraphAbsorber();
            absorber.Visit(doc); // processes all pages

            StringBuilder sb = new StringBuilder();

            // Iterate over each page's markup
            foreach (PageMarkup pageMarkup in absorber.PageMarkups)
            {
                // Sections contain paragraphs
                foreach (MarkupSection section in pageMarkup.Sections)
                {
                    foreach (MarkupParagraph paragraph in section.Paragraphs)
                    {
                        // Each paragraph provides its lines (list of text fragments)
                        foreach (var line in paragraph.Lines)
                        {
                            foreach (TextFragment fragment in line)
                            {
                                sb.Append(fragment.Text);
                            }
                            sb.AppendLine(); // preserve line break
                        }

                        // Add an extra blank line to separate paragraphs
                        sb.AppendLine();
                    }
                }
            }

            // Write the extracted text (with line breaks) to a .txt file
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Paragraph text extracted to '{outputPath}'.");
    }
}