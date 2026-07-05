using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_hyphenated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: create, load, save)
        using (Document doc = new Document(inputPath))
        {
            // Define a rectangle where the paragraph will be placed
            TextParagraph paragraph = new TextParagraph();
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(50, 500, 550, 700);

            // Enable automatic hyphenation by setting the wrap mode to DiscretionaryHyphenation
            paragraph.FormattingOptions = new TextFormattingOptions(TextFormattingOptions.WordWrapMode.DiscretionaryHyphenation);
            // Optional: customize the hyphen symbol (default is "-")
            paragraph.FormattingOptions.HyphenSymbol = "-";

            // Create a TextState for styling the inserted text
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Helvetica");
            textState.FontSize = 12;
            textState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Long text that will trigger hyphenation
            string longText = "Antidisestablishmentarianism is often used as an example of a very long word that may need hyphenation.";

            // Append the line with the defined TextState
            paragraph.AppendLine(longText, textState);

            // Add the paragraph to the first page using TextBuilder
            TextBuilder textBuilder = new TextBuilder(doc.Pages[1]);
            textBuilder.AppendParagraph(paragraph);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyphenated PDF saved to '{outputPath}'.");
    }
}