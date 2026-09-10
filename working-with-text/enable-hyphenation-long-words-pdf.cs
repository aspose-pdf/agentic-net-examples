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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a long text fragment that will require hyphenation
            TextFragment fragment = new TextFragment("ThisIsAnExcessivelyLongWordThatWillNeedHyphenationWhenWrapped");

            // Position the fragment on the page
            fragment.Position = new Position(100, 700);

            // Set font and size (optional)
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 12;

            // Enable hyphenation via FormattingOptions
            fragment.TextState.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;
            fragment.TextState.FormattingOptions.HyphenSymbol = "-";

            // Optionally set text color
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Append the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hyphenation enabled: '{outputPath}'");
    }
}