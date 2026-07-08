using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "paragraphs.txt";

        // Ensure the input PDF exists. If not, create a simple sample PDF.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
            Console.WriteLine($"Sample PDF created at '{inputPath}'.");
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Absorb paragraph structures from the whole document
            ParagraphAbsorber absorber = new ParagraphAbsorber();
            absorber.Visit(doc);

            // Write extracted paragraphs to a text file, preserving line breaks
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                foreach (PageMarkup pageMarkup in absorber.PageMarkups)
                {
                    foreach (MarkupSection section in pageMarkup.Sections)
                    {
                        foreach (MarkupParagraph paragraph in section.Paragraphs)
                        {
                            // Each paragraph may consist of multiple lines.
                            // Build the paragraph line by line.
                            foreach (var line in paragraph.Lines)
                            {
                                foreach (TextFragment fragment in line)
                                {
                                    writer.Write(fragment.Text);
                                }
                                writer.WriteLine(); // line break within paragraph
                            }

                            writer.WriteLine(); // blank line between paragraphs
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Paragraphs extracted to '{outputPath}'.");
    }

    private static void CreateSamplePdf(string path)
    {
        // Create a minimal PDF with a couple of paragraphs to demonstrate extraction.
        using (Document sampleDoc = new Document())
        {
            Page page = sampleDoc.Pages.Add();

            // First paragraph (two lines)
            TextFragment tf1 = new TextFragment("First paragraph line 1.");
            tf1.Position = new Position(0, 800);
            page.Paragraphs.Add(tf1);

            TextFragment tf2 = new TextFragment("First paragraph line 2.");
            tf2.Position = new Position(0, 780);
            page.Paragraphs.Add(tf2);

            // Add a small vertical gap to separate paragraphs
            TextFragment spacer = new TextFragment(" ");
            spacer.Position = new Position(0, 760);
            page.Paragraphs.Add(spacer);

            // Second paragraph (single line)
            TextFragment tf3 = new TextFragment("Second paragraph, single line.");
            tf3.Position = new Position(0, 740);
            page.Paragraphs.Add(tf3);

            sampleDoc.Save(path);
        }
    }
}
