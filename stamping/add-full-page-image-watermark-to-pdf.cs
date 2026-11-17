using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextState, FontStyles, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text stamp that will act as a watermark
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    // Make the stamp appear behind existing content
                    Background = true,

                    // Set opacity for a subtle watermark effect
                    Opacity = 0.2,

                    // Rotate to give a typical diagonal watermark look
                    RotateAngle = 45,

                    // Align to the center of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,

                    // Size the stamp to cover the whole page
                    Width  = page.PageInfo.Width,
                    Height = page.PageInfo.Height
                };

                // Configure the visual appearance of the text via the read‑only TextState object
                stamp.TextState.FontSize      = 72;
                stamp.TextState.FontStyle     = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Apply the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
