using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Create the text fragment to be placed
            TextFragment fragment = new TextFragment("Rotated Text");
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Rotate the text (degrees clockwise)
            fragment.TextState.Rotation = 45;

            // Position at the bottom‑right corner with a small margin
            const double margin = 20;
            double x = lastPage.PageInfo.Width - margin;   // right side
            double y = margin;                             // bottom side (origin is bottom‑left)
            fragment.Position = new Position(x, y);

            // Use TextBuilder to add the fragment to the page
            TextBuilder builder = new TextBuilder(lastPage);
            builder.AppendText(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added and saved to '{outputPath}'.");
    }
}