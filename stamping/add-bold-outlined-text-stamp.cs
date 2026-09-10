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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired text
            TextStamp stamp = new TextStamp("Bold Outline");

            // Position the stamp (centered on the page)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Render as graphic operators to respect rendering mode
            stamp.Draw = true;

            // Set fill (foreground) color and font properties
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48;

            // Enable fill‑then‑stroke rendering for bold outlined text
            stamp.TextState.RenderingMode = TextRenderingMode.FillThenStrokeText;

            // Outline (stroke) appearance
            stamp.OutlineWidth   = 1.5;   // stroke width
            stamp.OutlineOpacity = 1.0;   // fully opaque outline

            // Add the stamp to the first page of the document
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}