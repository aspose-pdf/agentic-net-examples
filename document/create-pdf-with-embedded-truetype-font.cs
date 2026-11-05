using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPath = "EmbeddedFont.pdf";
        const string trueTypeFontPath = "arial.ttf"; // Path to a TrueType font file

        if (!File.Exists(trueTypeFontPath))
        {
            Console.Error.WriteLine($"TrueType font not found: {trueTypeFontPath}");
            return;
        }

        // Register the TrueType font with the repository using a substitution
        // The first argument is the font family name to be used in the document,
        // the second argument is the path to the actual TTF file.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Arial", trueTypeFontPath));

        // Retrieve the font by its family name (e.g., "Arial")
        Font font = FontRepository.FindFont("Arial");
        if (font == null)
        {
            Console.Error.WriteLine("Failed to locate the registered font.");
            return;
        }

        // Ensure the font will be embedded in the PDF
        font.IsEmbedded = true;

        // Create the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a text fragment using the embedded TrueType font
            TextFragment tf = new TextFragment("This PDF uses an embedded TrueType font.");
            tf.TextState.Font = font;
            tf.TextState.FontSize = 12;

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // Save the PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with embedded TrueType font: {outputPath}");
    }
}
