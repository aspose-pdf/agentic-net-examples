using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Use ParagraphAbsorber to obtain markup information (including lines)
            ParagraphAbsorber absorber = new ParagraphAbsorber();
            absorber.Visit(doc.Pages[1]); // 1‑based page index

            // Iterate over each page markup (only one page was visited)
            foreach (PageMarkup pageMarkup in absorber.PageMarkups)
            {
                // Each page markup contains sections, each section contains paragraphs
                foreach (MarkupSection section in pageMarkup.Sections)
                {
                    foreach (MarkupParagraph paragraph in section.Paragraphs)
                    {
                        // The Lines property gives a list of lines; each line is a list of TextFragment objects
                        List<List<TextFragment>> lines = paragraph.Lines;

                        Console.WriteLine($"Paragraph with {lines.Count} line(s):");

                        for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
                        {
                            List<TextFragment> lineFragments = lines[lineIndex];

                            // Concatenate the text of all fragments in the current line
                            string lineText = string.Empty;
                            foreach (TextFragment frag in lineFragments)
                                lineText += frag.Text;

                            // Example custom layout processing:
                            // Identify if the line ends with an explicit line‑break character.
                            bool hasExplicitLineBreak = lineText.EndsWith("\r") ||
                                                        lineText.EndsWith("\n") ||
                                                        lineText.EndsWith("\r\n");

                            Console.WriteLine($"  Line {lineIndex + 1}: \"{lineText}\" " +
                                              $"(ExplicitBreak={hasExplicitLineBreak})");
                            
                            // Place custom logic here – e.g., modify fragment properties based on the flag
                            // if (hasExplicitLineBreak) { /* custom actions */ }
                        }
                    }
                }
            }

            // Save the (unchanged) document – explicit SaveOptions are not required for PDF output
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}