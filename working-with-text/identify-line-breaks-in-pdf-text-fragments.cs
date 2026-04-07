using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;   // Facades namespace as requested

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

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Use ParagraphAbsorber to obtain markup information, which includes
            // MarkupParagraph objects that expose the Lines property.
            ParagraphAbsorber absorber = new ParagraphAbsorber();
            absorber.Visit(doc.Pages[1]); // first page (1‑based indexing)

            // Ensure we have at least one markup paragraph
            if (absorber.PageMarkups.Count == 0 ||
                absorber.PageMarkups[0].Paragraphs.Count == 0)
            {
                Console.WriteLine("No paragraph markup found on the first page.");
                doc.Save(outputPath); // save unchanged document
                return;
            }

            // Get the first markup paragraph (you can iterate all if needed)
            MarkupParagraph markupParagraph = absorber.PageMarkups[0].Paragraphs[0];

            // Each line is a List<TextFragment>
            List<List<TextFragment>> lines = markupParagraph.Lines;

            Console.WriteLine($"Paragraph contains {lines.Count} line(s).");

            // Iterate through lines to identify line‑break flags.
            // For demonstration we treat a line that ends with a hyphen ('-')
            // as a soft line break (word hyphenation).
            for (int i = 0; i < lines.Count; i++)
            {
                List<TextFragment> lineFragments = lines[i];

                // Concatenate the text of all fragments that belong to this line
                string lineText = string.Empty;
                foreach (TextFragment frag in lineFragments)
                {
                    lineText += frag.Text;
                }

                // Determine if this line ends with a hyphen (soft break)
                bool endsWithHyphen = lineText.EndsWith("-");

                // Output diagnostic information
                Console.WriteLine($"Line {i + 1}: \"{lineText}\"");
                Console.WriteLine($"  Ends with hyphen (soft break): {endsWithHyphen}");

                // Example custom processing: if a soft break is detected,
                // remove the hyphen and join with the next line's first fragment.
                if (endsWithHyphen && i < lines.Count - 1)
                {
                    // Remove hyphen from the current line's last fragment
                    TextFragment lastFragment = lineFragments[lineFragments.Count - 1];
                    if (lastFragment.Text.EndsWith("-"))
                    {
                        lastFragment.Text = lastFragment.Text.TrimEnd('-');
                    }

                    // Prepend the first fragment of the next line to the current line
                    List<TextFragment> nextLineFragments = lines[i + 1];
                    if (nextLineFragments.Count > 0)
                    {
                        TextFragment firstNext = nextLineFragments[0];
                        lastFragment.Text += firstNext.Text;

                        // Remove the now‑merged fragment from the next line
                        nextLineFragments.RemoveAt(0);
                    }
                }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
    }
}