using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_subset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF (no special load options needed for plain PDF)
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Low‑level font handling
            // ------------------------------------------------------------
            // Find the custom font (replace "Arial" with the actual font name or path if needed)
            Font customFont = FontRepository.FindFont("Arial");
            // Mark the font to be embedded
            customFont.IsEmbedded = true;

            // Add a text fragment that uses the custom font – this forces the font to be part of the resources
            TextFragment tf = new TextFragment("Sample text using custom font");
            tf.TextState.Font = customFont;
            tf.Position = new Position(100, 700); // place near top of the first page
            doc.Pages[1].Paragraphs.Add(tf);

            // ------------------------------------------------------------
            // Subset the fonts – keep only glyphs actually used in the document
            // ------------------------------------------------------------
            // Subset all fonts (embedded and non‑embedded) to reduce file size
            doc.FontUtilities.SubsetFonts(FontSubsetStrategy.SubsetAllFonts);

            // ------------------------------------------------------------
            // Save the resulting PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with subsetted fonts to '{outputPath}'.");
    }
}