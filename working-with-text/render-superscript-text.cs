using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextBuilder to add text fragments to the page
            TextBuilder builder = new TextBuilder(page);

            // Normal part of the expression (e.g., "E = mc")
            TextFragment normal = new TextFragment("E = mc");
            normal.Position = new Position(100, 500);          // X, Y coordinates
            normal.TextState.Font = FontRepository.FindFont("Helvetica");
            normal.TextState.FontSize = 12;                    // Regular size
            builder.AppendText(normal);

            // Superscript part (e.g., "2")
            TextFragment superscript = new TextFragment("2");
            superscript.Position = new Position(0, 0);         // Continues from previous text
            superscript.TextState.Font = FontRepository.FindFont("Helvetica");
            superscript.TextState.FontSize = 8;                // Smaller size for superscript
            superscript.TextState.Superscript = true;         // Raise the text above baseline
            builder.AppendText(superscript);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Superscript text rendered and saved to '{outputPath}'.");
    }
}