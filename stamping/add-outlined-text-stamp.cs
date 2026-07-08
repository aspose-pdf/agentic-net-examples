using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for TextRenderingMode and FontRepository

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired text
            TextStamp stamp = new TextStamp("OUTLINE");

            // Set the rendering mode to StrokeText to produce outlined text
            stamp.TextState.RenderingMode = TextRenderingMode.StrokeText;

            // Optional: configure font and size
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48;

            // Ensure the stamp is drawn as graphic operators (required for rendering mode)
            stamp.Draw = true;

            // Position the stamp on the page (example coordinates)
            stamp.XIndent = 100; // horizontal position from left
            stamp.YIndent = 700; // vertical position from bottom

            // Add the stamp to the first page (Page.AddStamp is the correct method)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF (lifecycle rule: save within using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}