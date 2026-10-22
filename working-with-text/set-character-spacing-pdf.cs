using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string text       = "Sample spaced text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment that will hold the string
            TextFragment fragment = new TextFragment(text);

            // Set character spacing (in points) on the TextState before adding the fragment
            fragment.TextState.CharacterSpacing = 2.0f; // increase spacing by 2 points (float literal)

            // Optional: set other visual properties
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the text on the first page
            Page page = doc.Pages[1];
            fragment.Position = new Position(100, 700); // X=100, Y=700 points from bottom-left
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text with custom character spacing saved to '{outputPath}'.");
    }
}
