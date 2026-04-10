using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_subset.pdf";  // result PDF
        const string fontPath   = "customfont.ttf";     // custom TrueType font file

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Custom font not found: {fontPath}");
            return;
        }

        // Load the PDF document (using‑statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Load the custom TrueType font via FontRepository (Font has no public ctor)
            Font customFont = FontRepository.OpenFont(fontPath);
            // Mark the font to be embedded and subsetted
            customFont.IsEmbedded = true;
            customFont.IsSubset   = true;

            // Add a sample text fragment that uses the custom font
            TextFragment tf = new TextFragment("Sample text using custom font");
            tf.TextState.Font     = customFont;
            tf.TextState.FontSize = 12;
            tf.Position = new Position(100, 700); // place on page 1
            doc.Pages[1].Paragraphs.Add(tf);

            // Subset all fonts in the document (including the custom one)
            // This reduces file size by keeping only used glyphs.
            doc.FontUtilities.SubsetFonts(FontSubsetStrategy.SubsetAllFonts);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Subset PDF saved to '{outputPath}'.");
    }
}