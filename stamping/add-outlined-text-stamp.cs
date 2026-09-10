using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextState, TextRenderingMode, FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired value
            TextStamp stamp = new TextStamp("OUTLINE");

            // Draw the stamp as graphic operators (required for rendering mode)
            stamp.Draw = true;

            // Configure the text rendering mode to stroke (outline)
            stamp.TextState.RenderingMode = TextRenderingMode.StrokeText;

            // Optional styling: font, size, color, outline width
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            stamp.OutlineWidth = 1.0; // thickness of the outline

            // Position the stamp (centered on the page)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to the first page
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}