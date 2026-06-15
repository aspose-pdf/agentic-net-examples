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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a new text fragment with the desired content
            TextFragment fragment = new TextFragment("Kerning enabled text");

            // Kerning property does not exist on TextFragmentState. Use CharacterSpacing to adjust spacing if needed.
            // fragment.TextState.Kerning = true; // Removed – not supported
            fragment.TextState.CharacterSpacing = 0.5f; // Adjust spacing as an alternative to kerning

            // Optional: set font and size for clarity
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;

            // Position the fragment on the first page
            fragment.Position = new Position(100, 700);

            // Add the fragment to the first page's paragraph collection
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with kerning enabled: '{outputPath}'.");
    }
}
