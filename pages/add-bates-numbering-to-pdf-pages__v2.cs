using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextStamp

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create the Bates number: year prefix + zero‑padded page number
                string batesText = $"2026-{i:D4}";

                // Configure a TextStamp for the current page
                TextStamp stamp = new TextStamp(batesText)
                {
                    // Position the stamp at the bottom‑right corner
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Margins – use individual margin properties (no Margin object)
                    RightMargin = 20,
                    BottomMargin = 20,
                    // Appearance settings
                    Background = false,
                    Opacity    = 0.5,
                    // Use a cross‑platform color (avoid System.Drawing)
                    TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.DarkGray }
                };

                // Apply the stamp to the current page
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied and saved to '{outputPath}'.");
    }
}
