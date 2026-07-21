using System;
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new TextFragment with the desired text
            TextFragment fragment = new TextFragment("Kerning enabled text");

            // Kerning is not a separate property on TextFragmentState.
            // Use CharacterSpacing to adjust inter‑character spacing if needed.
            // Setting it to a small positive value can emulate kerning effects.
            fragment.TextState.CharacterSpacing = 0.5f;

            // Optional: adjust word spacing (if required)
            // fragment.TextState.WordSpacing = 1.0f;

            // Set the position where the text will be placed on the page
            fragment.Position = new Position(100, 700);

            // Add the fragment to the first page of the document
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with kerning enabled: '{outputPath}'.");
    }
}
