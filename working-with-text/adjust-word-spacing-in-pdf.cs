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
        const string textToInsert = "Sample text with custom spacing.";
        const float wordSpacing = 2.0f; // increase spacing between words

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Create a TextFragment with the desired text
                TextFragment fragment = new TextFragment(textToInsert);

                // Set font, size, and color
                fragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Arial");
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Adjust word spacing
                fragment.TextState.WordSpacing = wordSpacing;

                // Position the fragment on the first page (example coordinates)
                fragment.Position = new Aspose.Pdf.Text.Position(100, 700);

                // Add the fragment to the first page
                doc.Pages[1].Paragraphs.Add(fragment);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text inserted with custom word spacing saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}