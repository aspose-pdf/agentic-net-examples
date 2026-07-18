using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // needed for FontRepository, TextState, TextRenderingMode

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired text
            TextStamp stamp = new TextStamp("Bold Outline");

            // Render as graphic operators (required for fill‑stroke rendering)
            stamp.Draw = true;

            // Configure text appearance via TextState
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;   // fill color
            stamp.TextState.RenderingMode = TextRenderingMode.FillThenStrokeText; // fill + stroke
            stamp.OutlineWidth = 1.5;          // stroke thickness
            stamp.OutlineOpacity = 1.0;        // fully opaque outline

            // Position the stamp (centered on each page)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp with fill‑stroke rendering saved to '{outputPath}'.");
    }
}