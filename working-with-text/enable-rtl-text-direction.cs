using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_rtl.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set the overall document reading order to right‑to‑left (optional)
            doc.Direction = Aspose.Pdf.Direction.R2L;

            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a text fragment with Arabic sample text
            TextFragment fragment = new TextFragment("مرحبا بالعالم"); // "Hello World" in Arabic

            // Position the text on the page
            fragment.Position = new Position(100, 500);

            // Configure the text state
            TextState state = fragment.TextState;
            // Choose a font that supports Arabic characters
            state.Font = FontRepository.FindFont("Arial Unicode MS");
            state.FontSize = 14;
            state.ForegroundColor = Aspose.Pdf.Color.Black;
            // Note: The TextState class in the current Aspose.Pdf version does not expose a Direction property.
            // The document‑level Direction (doc.Direction) already ensures right‑to‑left rendering for supported fonts.

            // Append the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with right‑to‑left text: '{outputPath}'.");
    }
}
