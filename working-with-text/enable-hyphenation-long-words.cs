using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define a rectangle where the text will be placed
            // (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 500, 550, 750);

            // Create a TextParagraph to hold the text
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = rect
            };

            // Enable hyphenation by setting the word wrap mode to ByWords
            // This allows discretionary hyphenation when a word cannot fit.
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Optionally set a hyphen symbol (default is "-")
            paragraph.FormattingOptions.HyphenSymbol = "-";

            // Long word that will require hyphenation
            string longWord = "Supercalifragilisticexpialidocious";

            // Append the long word as a line
            paragraph.AppendLine(longWord);

            // Use TextBuilder to add the paragraph to the page
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the PDF
            string outputPath = "HyphenatedOutput.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}