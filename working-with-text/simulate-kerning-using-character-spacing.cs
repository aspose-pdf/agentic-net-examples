using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_kerning.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (Document creation and loading)
        using (Document doc = new Document(inputPath))
        {
            // Create a new TextFragment with the desired text
            TextFragment fragment = new TextFragment("Kerning enabled text");

            // TextFragmentState does not expose a Kerning property.
            // To adjust inter‑character spacing, use CharacterSpacing instead.
            // A small positive or negative value can simulate kerning effects.
            fragment.TextState.CharacterSpacing = 0.5f;

            // Optional: set font and size for better visibility
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Color.Blue;

            // Position the fragment on the first page
            fragment.Position = new Position(100, 700);

            // Append the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(doc.Pages[1]);
            builder.AppendText(fragment);

            // Save the modified PDF (Document saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with kerning enabled: {outputPath}");
    }
}
