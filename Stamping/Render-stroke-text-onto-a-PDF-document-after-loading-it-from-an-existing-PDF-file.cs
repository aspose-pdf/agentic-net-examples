using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_stroked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the stroked text will be rendered (first page in this example)
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Stroked Text");

            // Set the position where the text will appear (coordinates are from bottom‑left)
            fragment.Position = new Position(100, 600);

            // Configure visual appearance: stroke rendering, font, size and color
            fragment.TextState.FontSize = 36;
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.ForegroundColor = Color.Black; // color of the stroke
            // Correct property name for rendering mode
            fragment.TextState.RenderingMode = TextRenderingMode.StrokeText;

            // Use TextBuilder to append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stroked text added and saved to '{outputPath}'.");
    }
}
