using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_numbered.pdf";
        const string fontPath = "customfont.ttf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Register the custom TrueType font via substitution (AddFont was removed in newer versions)
        string familyName = Path.GetFileNameWithoutExtension(fontPath);
        FontRepository.Substitutions.Add(new SimpleFontSubstitution(familyName, fontPath));

        // Retrieve the font instance
        Font customFont = FontRepository.FindFont(familyName);
        if (customFont == null)
        {
            Console.Error.WriteLine("Failed to load custom font.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment with the page number
                TextFragment tf = new TextFragment(i.ToString())
                {
                    TextState =
                    {
                        Font = customFont,
                        FontSize = 12,
                        ForegroundColor = Color.Black
                    }
                };

                // Position the number at the bottom‑right corner
                double margin = 10; // points
                double x = page.PageInfo.Width - margin;
                double y = margin;
                tf.Position = new Position(x, y);

                // Add the fragment to the page
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
