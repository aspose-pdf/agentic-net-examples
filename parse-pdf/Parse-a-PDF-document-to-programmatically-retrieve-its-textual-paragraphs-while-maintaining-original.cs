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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a ParagraphAbsorber to detect sections and paragraphs
            ParagraphAbsorber absorber = new ParagraphAbsorber();

            // Perform the search on the whole document
            absorber.Visit(doc);

            // List to hold extracted paragraphs in reading order
            List<string> paragraphs = new List<string>();

            // Iterate over each page's markup (pages are processed in document order)
            foreach (PageMarkup pageMarkup in absorber.PageMarkups)
            {
                // Sections are ordered as they appear on the page
                foreach (MarkupSection section in pageMarkup.Sections)
                {
                    // Paragraphs within a section are ordered top‑to‑bottom, left‑to‑right
                    foreach (MarkupParagraph paragraph in section.Paragraphs)
                    {
                        // The Text property contains the paragraph's full text
                        string paraText = paragraph.Text?.Trim();

                        if (!string.IsNullOrEmpty(paraText))
                        {
                            paragraphs.Add(paraText);
                        }
                    }
                }
            }

            // Output the paragraphs preserving the original order
            Console.WriteLine("Extracted paragraphs (in reading order):");
            for (int i = 0; i < paragraphs.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {paragraphs[i]}");
            }
        }
    }
}