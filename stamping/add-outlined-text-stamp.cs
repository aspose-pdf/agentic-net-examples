using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TextState, FontRepository

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired text
            TextStamp textStamp = new TextStamp("OUTLINED TEXT");

            // The current Aspose.Pdf version does not expose TextRenderingMode on TextState.
            // To achieve an outlined‑like appearance, use a bold font (or a heavier weight).
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            textStamp.TextState.FontSize = 48;
            textStamp.TextState.ForegroundColor = Color.Black; // stroke/fill color

            // Optional visual settings
            textStamp.Opacity = 1.0;      // fully opaque
            textStamp.Background = false; // draw on top of page content
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to the first page (or iterate pages as needed)
            Page page = doc.Pages[1];
            page.AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
