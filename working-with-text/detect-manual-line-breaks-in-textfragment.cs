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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all paragraphs on the page
                foreach (var paragraph in page.Paragraphs)
                {
                    // In the current Aspose.Pdf API the concrete paragraph type that
                    // contains text is TextFragment.  It does not expose a Lines
                    // collection, so we infer logical lines by splitting the text on
                    // newline characters.
                    if (paragraph is TextFragment textFragment)
                    {
                        // Split the fragment's text into logical lines.
                        string[] lines = textFragment.Text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

                        for (int lineIdx = 0; lineIdx < lines.Length; lineIdx++)
                        {
                            string line = lines[lineIdx];

                            // Determine whether the original line ended with a manual line‑break.
                            // Because the split operation removes the delimiter, we need to
                            // look at the original text to see what delimiter was used.
                            // For simplicity we treat any of the recognised delimiters as a break.
                            bool lineEndsWithBreak = false;
                            int startPos = 0;
                            for (int i = 0; i < lineIdx; i++)
                                startPos += lines[i].Length + 1; // +1 approximates the removed delimiter
                            if (startPos + line.Length < textFragment.Text.Length)
                            {
                                char nextChar = textFragment.Text[startPos + line.Length];
                                lineEndsWithBreak = nextChar == '\n' || nextChar == '\r';
                            }

                            Console.WriteLine($"Page {pageIndex}, TextFragment ID {textFragment.GetHashCode()}, Line {lineIdx + 1}/{lines.Length}, EndsWithBreak={lineEndsWithBreak}");
                            Console.WriteLine($"    Line text: \"{line}\"");
                        }
                    }
                }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
