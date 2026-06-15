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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Adjusted word spacing text");

            // Configure the TextState's WordSpacing property (float value)
            fragment.TextState.WordSpacing = 2.0f; // increase spacing between words

            // Optionally set other text properties (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the fragment on the first page using Position (not Point)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page's paragraphs collection
            Page page = doc.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with adjusted word spacing to '{outputPath}'.");
    }
}