using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hyphenated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextParagraph and define its bounding rectangle
            TextParagraph paragraph = new TextParagraph();
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(50, 500, 550, 750);

            // Enable automatic hyphenation:
            // - Set a wrap mode that allows breaking words (ByWords or DiscretionaryHyphenation)
            // - Define the hyphen symbol to be used during hyphenation
            paragraph.FormattingOptions = new TextFormattingOptions(TextFormattingOptions.WordWrapMode.ByWords);
            paragraph.FormattingOptions.HyphenSymbol = "-";

            // Add a long line of text that will trigger hyphenation when it doesn't fit
            string longText = "Supercalifragilisticexpialidocious is a very long word that might need hyphenation when it does not fit in the line.";
            paragraph.AppendLine(longText);

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF (using the required lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyphenated PDF saved to '{outputPath}'.");
    }
}