using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Use ParagraphAbsorber to get the logical structure of the page(s)
            ParagraphAbsorber paraAbsorber = new ParagraphAbsorber();
            // Absorb the first page (change index as needed)
            paraAbsorber.Visit(doc.Pages[1]);

            // Iterate over each markup (there will be one per visited page)
            foreach (PageMarkup pageMarkup in paraAbsorber.PageMarkups)
            {
                // Iterate over each paragraph in the page
                foreach (MarkupParagraph paragraph in pageMarkup.Sections[0].Paragraphs)
                {
                    // Each paragraph may consist of multiple lines.
                    // Lines property: List<List<TextFragment>>
                    List<List<TextFragment>> lines = paragraph.Lines;

                    Console.WriteLine($"Paragraph with {lines.Count} line(s):");

                    // Iterate through lines
                    for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
                    {
                        List<TextFragment> lineFragments = lines[lineIndex];
                        Console.Write($"  Line {lineIndex + 1}: ");

                        // Concatenate the text of all fragments in the line
                        string lineText = string.Empty;
                        foreach (TextFragment frag in lineFragments)
                        {
                            lineText += frag.Text;
                        }

                        Console.WriteLine(lineText);

                        // Example of custom layout processing:
                        // Identify if the line ends with a hard line break.
                        // Aspose.Pdf does not expose a direct "IsHardBreak" flag,
                        // but a common heuristic is to check the last fragment's
                        // TextState or the presence of a newline character.
                        bool endsWithHardBreak = false;
                        if (lineFragments.Count > 0)
                        {
                            TextFragment lastFrag = lineFragments[lineFragments.Count - 1];
                            // Check for explicit newline characters in the fragment text
                            endsWithHardBreak = lastFrag.Text.EndsWith("\r") ||
                                                lastFrag.Text.EndsWith("\n") ||
                                                lastFrag.Text.EndsWith("\r\n");
                        }

                        Console.WriteLine($"    Hard break detected: {endsWithHardBreak}");
                    }
                }
            }

            // (Optional) Save the document if any modifications were made.
            doc.Save(outputPath);
            Console.WriteLine($"Processed document saved to '{outputPath}'.");
        }
    }
}