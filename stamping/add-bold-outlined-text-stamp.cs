using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_bold_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify, and save – all within using blocks for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired text
            TextStamp stamp = new TextStamp("Bold Outlined Text")
            {
                // Render as graphic operators (required for fill‑stroke rendering)
                Draw = true,
                // Position the stamp (example: centered horizontally, 100 points from bottom)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                BottomMargin        = 100
            };

            // Configure text appearance – use a bold font to simulate an outlined effect
            stamp.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Yellow; // Fill color

            // Optional: control outline thickness and opacity (if supported by the version)
            stamp.OutlineWidth   = 2.0;   // thickness of the outline
            stamp.OutlineOpacity = 1.0;   // fully opaque outline

            // Add the stamp to the first page (page indexing is 1‑based)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with bold outlined text stamp to '{outputPath}'.");
    }
}
