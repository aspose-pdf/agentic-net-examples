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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired string.
            TextStamp stamp = new TextStamp("Bold Outlined Text");

            // Render the stamp as graphic operators (required for fill‑stroke rendering).
            stamp.Draw = true;

            // Position the stamp – here we use absolute coordinates.
            stamp.XIndent = 100;   // distance from the left edge
            stamp.YIndent = 700;   // distance from the bottom edge

            // Configure the visual appearance via the TextState object.
            // Use a bold font to simulate an outlined (bold) effect because the current
            // Aspose.Pdf version does not expose StrokeColor or TextRenderingMode on TextState.
            stamp.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // fill color

            // Add the stamp to the first page (pages are 1‑based).
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp saved to '{outputPath}'.");
    }
}
