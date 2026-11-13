using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_column.pdf";

        // Sample long text to flow into columns
        string sampleText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                            "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                            "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                            "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Page dimensions
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Layout configuration
            int columnCount = 2;          // number of columns
            double margin = 50;           // page margin on all sides
            double columnGap = 20;        // space between columns
            double columnWidth = (pageWidth - 2 * margin - (columnCount - 1) * columnGap) / columnCount;
            double startY = pageHeight - margin; // top start position
            double lineHeight = 14;       // approximate line height for 12pt font

            // Simple word‑wrapping logic
            string[] words = sampleText.Split(' ');
            int currentColumn = 0;
            double cursorX = margin;
            double cursorY = startY;
            string line = "";

            foreach (string word in words)
            {
                string testLine = string.IsNullOrEmpty(line) ? word : line + " " + word;
                // Rough width estimate: 7 units per character for 12pt font
                double estimatedWidth = testLine.Length * 7;

                if (estimatedWidth > columnWidth && !string.IsNullOrEmpty(line))
                {
                    // Emit the completed line
                    TextFragment tf = new TextFragment(line);
                    tf.Position = new Position(cursorX, cursorY);
                    tf.TextState.FontSize = 12;
                    page.Paragraphs.Add(tf);

                    // Move down one line
                    cursorY -= lineHeight;

                    // If bottom margin reached, switch to next column
                    if (cursorY < margin)
                    {
                        currentColumn++;
                        if (currentColumn >= columnCount) break; // no more columns available
                        cursorX = margin + currentColumn * (columnWidth + columnGap);
                        cursorY = startY;
                    }

                    line = word; // start new line with current word
                }
                else
                {
                    line = testLine;
                }
            }

            // Add any remaining text
            if (!string.IsNullOrEmpty(line) && currentColumn < columnCount && cursorY >= margin)
            {
                TextFragment tf = new TextFragment(line);
                tf.Position = new Position(cursorX, cursorY);
                tf.TextState.FontSize = 12;
                page.Paragraphs.Add(tf);
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi‑column PDF saved to '{outputPath}'.");
    }
}