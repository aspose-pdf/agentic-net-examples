using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
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
            TextStamp textStamp = new TextStamp("Sample Text");

            // Configure the font, size, and color (using Aspose.Pdf.Color for cross‑platform compatibility)
            Font font = FontRepository.FindFont("Arial"); // fallback to a standard font if available
            textStamp.TextState.Font = font;
            textStamp.TextState.FontSize = 24; // custom size
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // custom blue color

            // Position the stamp on the page
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Top;
            // Use XIndent/YIndent instead of the non‑existent Margin property
            textStamp.XIndent = 0;   // centered, so no horizontal offset
            textStamp.YIndent = 20;  // 20 points from the top edge

            // Apply the stamp only to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}
