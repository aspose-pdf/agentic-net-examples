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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, add a text fragment with adjusted character spacing (kerning is not a direct property), and save.
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment containing the desired text.
            TextFragment tf = new TextFragment("Kerning enabled text");

            // Kerning cannot be toggled via a Kerning property on TextFragmentState.
            // Use CharacterSpacing to fine‑tune inter‑character spacing, which approximates kerning.
            tf.TextState.CharacterSpacing = 0.5f; // adjust as needed for the desired visual effect

            // Optional: set font and size.
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.FontSize = 12;

            // Add the fragment to the first page (pages are 1‑based).
            Page page = doc.Pages[1];
            page.Paragraphs.Add(tf);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with adjusted character spacing to '{outputPath}'.");
    }
}
