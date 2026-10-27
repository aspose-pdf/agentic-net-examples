using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_subset.pdf";
        const string fontPath  = "MyCustomFont.ttf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the source PDF inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Load the custom TrueType font via the low‑level FontRepository API
            // OpenFont returns a Font object that can be configured for embedding/subsetting
            Font customFont = FontRepository.OpenFont(fontPath);

            // Request embedding and enable subset mode to keep only used glyphs
            customFont.IsEmbedded = true;   // embed the font into the PDF
            customFont.IsSubset   = true;   // embed only the glyphs that are actually used

            // Create a text fragment that uses the subset font
            TextFragment fragment = new TextFragment("Hello, subset font!");
            fragment.TextState.Font = customFont;
            fragment.TextState.FontSize = 12;

            // Use 1‑based page indexing (first page is Pages[1])
            Page page = doc.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with subset font: {outputPdf}");
    }
}
