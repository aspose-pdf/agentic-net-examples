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
            // Create a new text fragment to insert
            TextFragment fragment = new TextFragment("Sample spaced text");

            // Adjust word spacing (e.g., 2.0 units)
            fragment.TextState.WordSpacing = 2.0f;

            // Optional: set additional text properties
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the text on the first page
            Page page = doc.Pages[1];
            fragment.Position = new Position(100, 700);
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}