using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_text.pdf";
        const string text = "Rotated Text Example";
        const float angleDegrees = 45f;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment(text);

            // Set font, size, and color
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 24F; // float literal
            fragment.TextState.ForegroundColor = Color.FromRgb(0, 0, 255); // Blue

            // Rotate the text – TextState has a Rotation property (degrees)
            fragment.TextState.Rotation = angleDegrees;

            // Position the text fragment on the page (lower‑left corner of the text)
            fragment.Position = new Position(100, 500);

            // Add the fragment to the first page
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added and saved to '{outputPath}'.");
    }
}
