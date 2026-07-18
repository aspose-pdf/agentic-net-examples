using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextStamp, FontRepository, TextState
using System.Drawing; // System.Drawing.Color for convenience

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Iterate over pages and add the rotated stamp to odd‑numbered pages
            foreach (Page page in doc.Pages)
            {
                if (page.Number % 2 == 1) // odd page
                {
                    // Create a TextStamp (simpler than Facades.Stamp for this scenario)
                    TextStamp textStamp = new TextStamp(stampText);

                    // Configure appearance
                    textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                    textStamp.TextState.FontSize = 24;
                    textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                    // Use RotateAngle instead of the non‑existent Rotation property
                    textStamp.RotateAngle = 30f; // 30 degree rotation

                    // Position: left margin (10 points from left) and vertically centered
                    textStamp.HorizontalAlignment = HorizontalAlignment.Left;
                    textStamp.XIndent = 10f; // distance from left edge
                    textStamp.VerticalAlignment = VerticalAlignment.Center;
                    // YIndent left at default (0) – Center alignment will place it in the middle of the page

                    // Add the stamp to the current page
                    page.AddStamp(textStamp);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text stamp applied. Output saved to '{outputPath}'.");
    }
}
