using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // needed for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing is handled by the foreach)
            foreach (Page page in doc.Pages)
            {
                // Define step size for repeating the watermark across the page
                const float stepX = 200f;   // horizontal distance between stamps
                const float stepY = 150f;   // vertical distance between stamps

                // Loop over a grid covering the page area
                for (float x = 0; x < page.Rect.Width; x += stepX)
                {
                    for (float y = 0; y < page.Rect.Height; y += stepY)
                    {
                        // Create a TextStamp with the desired text
                        TextStamp stamp = new TextStamp(watermarkText);

                        // Position the stamp at the current grid point
                        stamp.XIndent = x;
                        stamp.YIndent = y;

                        // Rotate the stamp to create a diagonal effect
                        stamp.RotateAngle = -45; // negative for bottom‑left to top‑right

                        // Make the stamp appear behind page content
                        stamp.Background = true;

                        // Set opacity so the watermark is faint
                        stamp.Opacity = 0.2f; // range 0.0 – 1.0

                        // Align the stamp (centered on its own rectangle)
                        stamp.HorizontalAlignment = HorizontalAlignment.Center;
                        stamp.VerticalAlignment   = VerticalAlignment.Center;

                        // Define visual style of the text
                        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                        stamp.TextState.FontSize = 48;
                        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                        // Add the stamp to the current page
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}