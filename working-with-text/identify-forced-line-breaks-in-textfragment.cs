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

        // Load the PDF document – wrap in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                Console.WriteLine($"--- Page {pageIndex} ---");

                // Each page contains a collection of paragraphs.
                // In Aspose.Pdf a paragraph that holds text is a TextFragment.
                // TextFragment does not expose a Lines collection; instead we treat the
                // fragment's Text as a whole and split it into logical lines ourselves.
                foreach (var paragraphObj in page.Paragraphs)
                {
                    if (paragraphObj is TextFragment textFragment)
                    {
                        // Split the fragment's text on line‑break characters.
                        // This gives us the visual lines that were originally present
                        // in the PDF (Aspose keeps the original line‑breaks in the Text).
                        string[] lines = textFragment.Text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

                        for (int i = 0; i < lines.Length; i++)
                        {
                            string lineText = lines[i];

                            // Example of a custom line‑break flag:
                            // If the original line ends with a manual line‑break character
                            // (e.g., a forced '\n' that was not just a wrap), we treat it as a forced break.
                            // Since we already split on line‑breaks, we can infer a forced break by
                            // checking whether the original fragment ended with a newline after this line.
                            bool hasForcedBreak = (i < lines.Length - 1) || textFragment.Text.EndsWith("\n");

                            Console.WriteLine($"Line {i + 1}: \"{lineText}\"  ForcedBreak={hasForcedBreak}");
                        }
                    }
                }
            }
        }
    }
}
