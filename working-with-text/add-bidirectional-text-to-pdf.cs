using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "bidi_output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a new page where the bidirectional text will be placed
            Page page = doc.Pages.Add();

            // Example bidirectional text: Arabic phrase with English word
            // The Unicode Right-to-Left Mark (U+200F) forces RTL rendering
            string bidiText = "\u200Fمرحبا world!";

            // Create a TextFragment with the bidi text
            TextFragment fragment = new TextFragment(bidiText);

            // Position the fragment on the page (X = 100, Y = 700)
            fragment.Position = new Position(100, 700);

            // Configure the text appearance
            // Use a font that supports Arabic characters (e.g., Arial Unicode MS)
            Font arialUnicode = FontRepository.FindFont("Arial Unicode MS");
            fragment.TextState.Font = arialUnicode;
            fragment.TextState.FontSize = 14;
            fragment.TextState.ForegroundColor = Color.Black;

            // Add the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bidirectional text PDF saved to '{outputPath}'.");
    }
}