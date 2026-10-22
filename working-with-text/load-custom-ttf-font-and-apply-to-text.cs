using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the custom TrueType font file and the output PDF
        const string fontPath   = "custom.ttf";
        const string outputPath = "output.pdf";

        // Verify that the font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Register the custom font with the FontRepository using a substitution.
        // The name "CustomFont" can be any identifier you prefer.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("CustomFont", fontPath));

        // Retrieve the registered font instance.
        Font customFont = FontRepository.FindFont("CustomFont");

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment("Hello, world with a custom TrueType font!");

            // Modify the existing TextState (the property is read‑only)
            fragment.TextState.Font = customFont;
            fragment.TextState.FontSize = 14;
            fragment.TextState.ForegroundColor = Color.Black;

            // Position the fragment on the page
            fragment.Position = new Position(100, 700); // X=100, Y=700

            // Add the fragment to the page's paragraphs collection
            page.Paragraphs.Add(fragment);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created successfully at '{outputPath}'.");
    }
}
