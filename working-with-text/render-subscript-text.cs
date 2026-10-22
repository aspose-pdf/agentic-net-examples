using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "subscript_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages[1];

            // Base coordinates for the formula (adjust as needed)
            double startX = 100;      // X‑position of the first character
            double baselineY = 700;   // Y‑position of the normal baseline

            // "H" fragment (normal text)
            var hFragment = new TextFragment("H");
            hFragment.TextState.FontSize = 12;               // normal size
            hFragment.Position = new Position(startX, baselineY);
            page.Paragraphs.Add(hFragment);

            // Subscript "2" fragment
            var subFragment = new TextFragment("2");
            subFragment.TextState.FontSize = 8;               // smaller size for subscript
            // Lower the baseline to create a subscript effect
            subFragment.Position = new Position(startX + 12, baselineY - 4);
            page.Paragraphs.Add(subFragment);

            // "O" fragment (normal text)
            var oFragment = new TextFragment("O");
            oFragment.TextState.FontSize = 12;
            oFragment.Position = new Position(startX + 20, baselineY);
            page.Paragraphs.Add(oFragment);

            // Save the modified document (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Subscript PDF saved to '{outputPath}'.");
    }
}
