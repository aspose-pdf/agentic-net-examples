using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Use TextFragmentAbsorber to collect all text fragments in the document
        TextFragmentAbsorber absorber = new TextFragmentAbsorber();
        pdfDocument.Pages.Accept(absorber);

        // Define the new font properties
        // Font: Arial, Size: 12 points, Style: Bold + Italic, Color: Blue
        var newFont = FontRepository.FindFont("Arial");
        const float newSize = 12f;
        const FontStyles newStyle = FontStyles.Bold | FontStyles.Italic;
        var newColor = Color.FromRgb(0, 0, 1); // Blue (RGB values are 0‑1 range)

        // Apply the font properties to every text fragment
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            fragment.TextState.Font = newFont;
            fragment.TextState.FontSize = newSize;
            fragment.TextState.FontStyle = newStyle;
            fragment.TextState.ForegroundColor = newColor;
        }

        // Save the modified PDF document
        pdfDocument.Save(outputPath);
    }
}