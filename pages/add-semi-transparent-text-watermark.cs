using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_output.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a textual stamp with the desired watermark text
                TextStamp stamp = new TextStamp(watermarkText);

                // Configure text appearance
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 48;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;   // Fill color

                // Semi‑transparent fill
                stamp.Opacity = 0.5;               // 0.0 (fully transparent) to 1.0 (opaque)

                // Outline (stroke) settings
                stamp.OutlineOpacity = 0.5;        // Semi‑transparent outline
                stamp.OutlineWidth = 1.0;          // Outline thickness

                // Position the watermark at the center of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}