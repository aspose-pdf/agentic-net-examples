using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_ligatures.pdf";

        // If the input file does not exist, create a simple one-page PDF.
        if (!File.Exists(inputPath))
        {
            using (Document newDoc = new Document())
            {
                newDoc.Pages.Add();
                newDoc.Save(inputPath);
            }
        }

        // Load the PDF, configure text state, add text, and save.
        using (Document doc = new Document(inputPath))
        {
            // Sample text that contains ligatures (e.g., "office").
            TextFragment tf = new TextFragment("office");

            // Aspose.Pdf does not expose a Ligatures property. Ligatures are handled automatically
            // by the font rendering engine. If fine‑tuning of character spacing is required, use
            // CharacterSpacing (or WordSpacing) instead.
            // tf.TextState.Ligatures = true; // <-- removed (property does not exist)
            tf.TextState.CharacterSpacing = 0; // optional: keep default spacing

            // Optional styling.
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 24;

            // Position the text on the page.
            tf.Position = new Position(100, 700);

            // Add the fragment to the first page.
            doc.Pages[1].Paragraphs.Add(tf);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ligatures (handled automatically by the font): {outputPath}");
    }
}
