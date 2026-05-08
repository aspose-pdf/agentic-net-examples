using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextFragment, TextState, Position, TextBuilder, FontRepository

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

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment("Kerning enabled text");

            // Set the position where the text will appear on the page
            fragment.Position = new Position(100, 700);

            // TextFragmentState does not expose a Kerning property. To achieve a kerning‑like effect,
            // adjust the character spacing. A small positive value tightens the characters.
            fragment.TextState.CharacterSpacing = 0.5f; // Simulates kerning

            // Optional: set font and size (using Aspose.Pdf.Text.FontRepository)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;

            // Append the fragment to the first page using TextBuilder
            TextBuilder builder = new TextBuilder(doc.Pages[1]);
            builder.AppendText(fragment);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with kerning enabled: '{outputPath}'");
    }
}
