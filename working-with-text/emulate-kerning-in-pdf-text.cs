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
        const string sampleText = "Kerning enabled text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment containing the desired text
            TextFragment fragment = new TextFragment(sampleText);

            // Aspose.Pdf does not expose a Kerning property on TextFragmentState.
            // To adjust spacing between characters, use CharacterSpacing instead.
            fragment.TextState.CharacterSpacing = 0.5f;

            // Optional: set position on the page (example coordinates)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the first page's paragraph collection
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with simulated kerning: {outputPath}");
    }
}
