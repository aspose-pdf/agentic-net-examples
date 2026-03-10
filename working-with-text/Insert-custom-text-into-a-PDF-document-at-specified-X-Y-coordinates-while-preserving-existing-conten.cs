using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to modify (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Custom inserted text");

            // Set the position (X, Y) where the text will appear
            // Position uses Aspose.Pdf.Text.Position (not System.Drawing.Point)
            fragment.Position = new Position(100, 500); // X = 100, Y = 500

            // Optional: set text appearance (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create a TextBuilder for the target page
            TextBuilder builder = new TextBuilder(page);

            // Append the text fragment; existing page content is preserved
            builder.AppendText(fragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text inserted and saved to '{outputPath}'.");
    }
}