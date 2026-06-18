using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "processed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing rule)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate through all paragraphs on the page
                foreach (BaseParagraph paragraph in page.Paragraphs)
                {
                    // The paragraph that contains text is a TextFragment (not MarkupParagraph)
                    if (paragraph is TextFragment textFragment)
                    {
                        Console.WriteLine($"Page {pageNum}, Paragraph: {textFragment.Text}");

                        // Split the fragment text into logical lines. Aspose.Pdf's TextFragment does not expose a Lines collection,
                        // so we derive line information by splitting on standard line‑break characters.
                        string[] lines = textFragment.Text?.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None) ?? Array.Empty<string>();

                        for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
                        {
                            string line = lines[lineIndex];
                            // A line ends with a line‑break if it is not the last line in the split array.
                            bool endsWithLineBreak = lineIndex < lines.Length - 1;

                            Console.WriteLine($"  Line {lineIndex + 1}:");
                            Console.WriteLine($"    Line text: \"{line}\"");
                            Console.WriteLine($"    Ends with line break: {endsWithLineBreak}");

                            // Additional custom logic can be placed here,
                            // e.g., adjusting positions, applying styles, etc.
                        }
                    }
                }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}
