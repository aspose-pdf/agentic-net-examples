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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired text value
            TextStamp stamp = new TextStamp("Outlined Text");

            // Draw as graphic operators (required for stroke rendering)
            stamp.Draw = true;

            // Center the stamp on the page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Configure text appearance – use a bold font to simulate an outline effect
            stamp.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Optional: increase outline width for a thicker outline (kept for compatibility)
            stamp.OutlineWidth = 2.0f;

            // Apply the stamp to each page (or target specific pages)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
