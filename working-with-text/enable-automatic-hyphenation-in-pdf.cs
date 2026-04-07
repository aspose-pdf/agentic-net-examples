using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hyphenated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare a long text that may need hyphenation
            string longText = "Antidisestablishmentarianism is a long word that might need to be hyphenated when it reaches the end of a line in a PDF document.";

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment(longText);

            // Position the fragment on the page (example coordinates)
            fragment.Position = new Position(50, 750);

            // Enable automatic hyphenation for this fragment.
            // The Hyphenation and HyphenSymbol properties were introduced in newer versions of Aspose.Pdf.
            // To keep the code compatible with older versions, we set them via reflection – if the properties exist they will be applied.
            var textState = fragment.TextState;
            var hyphenProp = textState.GetType().GetProperty("Hyphenation");
            if (hyphenProp != null && hyphenProp.CanWrite)
                hyphenProp.SetValue(textState, true);

            var hyphenSymbolProp = textState.GetType().GetProperty("HyphenSymbol");
            if (hyphenSymbolProp != null && hyphenSymbolProp.CanWrite)
                hyphenSymbolProp.SetValue(textState, "-");

            // Optionally set other visual properties
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to the first page using TextBuilder
            Page page = doc.Pages[1];
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyphenated PDF saved to '{outputPath}'.");
    }
}
