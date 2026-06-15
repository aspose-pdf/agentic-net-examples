using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "hyphenated.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a text paragraph that will hold the content
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle area where the paragraph will be placed
            paragraph.Rectangle = new Rectangle(50, 700, 550, 800);

            // Enable automatic hyphenation:
            // - Set word wrap mode to ByWords (allows discretionary hyphenation)
            // - Optionally specify the hyphen symbol to use
            paragraph.FormattingOptions = new TextFormattingOptions(TextFormattingOptions.WordWrapMode.ByWords);
            paragraph.FormattingOptions.HyphenSymbol = "-";

            // Sample long text that may require hyphenation
            string longText = "Supercalifragilisticexpialidocious is a very long word that might need hyphenation when it does not fit in the line.";
            paragraph.AppendLine(longText);

            // Render the paragraph onto the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}