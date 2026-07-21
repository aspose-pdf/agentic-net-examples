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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Example bidirectional (Arabic) text – Unicode string
            string arabicText = "مرحبا بالعالم"; // "Hello World" in Arabic (right‑to‑left)

            // Create a TextFragment with the Arabic text
            TextFragment tf = new TextFragment(arabicText);

            // Position the fragment on the page (X, Y coordinates)
            tf.Position = new Position(100, 700);

            // Configure the text appearance via TextState
            tf.TextState.Font = FontRepository.FindFont("Arial Unicode MS"); // Font that supports Arabic glyphs
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Color.Black;

            // Aspose.Pdf automatically handles bidirectional rendering based on the Unicode
            // directionality of the characters. No explicit Bidi property is required.

            // Add the fragment to the page
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bidirectional text PDF saved to '{outputPath}'.");
    }
}