using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_kerning.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Kerning enabled text");

            // Aspose.Pdf does not expose a Kerning flag on TextState.
            // Use CharacterSpacing to emulate kerning between characters.
            fragment.TextState.CharacterSpacing = 0.5f; // Adjust as needed

            // Set the position where the text will appear
            fragment.Position = new Position(100, 700);

            // Add the fragment to the first page of the document
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Kerning‑emulated PDF saved to '{outputPath}'.");
    }
}
