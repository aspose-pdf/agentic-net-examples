using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the TrueType font file (replace with your actual font file)
        const string fontFilePath = "custom.ttf";

        // Verify the font file exists
        if (!File.Exists(fontFilePath))
        {
            Console.Error.WriteLine($"Font file not found: {fontFilePath}");
            return;
        }

        // Load the font bytes (simulating loading from a memory stream)
        byte[] fontBytes = File.ReadAllBytes(fontFilePath);
        using (MemoryStream fontStream = new MemoryStream(fontBytes))
        {
            // Register the custom font using FontRepository substitutions (AddFont is obsolete)
            // The substitution maps a logical font name to the actual font file path.
            const string logicalFontName = "MyCustomFont";
            FontRepository.Substitutions.Add(new SimpleFontSubstitution(logicalFontName, fontFilePath));

            // Retrieve the Font object from the repository
            Font customFont = FontRepository.FindFont(logicalFontName);

            // Prepare a TextFragment and configure its TextState (TextState is read‑only, modify the instance)
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf with a custom TrueType font!");
            fragment.TextState.Font = customFont;
            fragment.TextState.FontSize = 24F;
            fragment.TextState.ForegroundColor = Color.DarkBlue;

            // Create a PDF document and add the text fragment to the first page
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(fragment);
                doc.Save("output.pdf");
            }

            Console.WriteLine("PDF created successfully with the custom TrueType font.");
        }
    }
}
