using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // FontRepository, TextState
using Aspose.Pdf.Drawing;      // Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a repeating text stamp to every page.
            // Because TextStamp does not support cloning, we create a fresh instance for each page.
            foreach (Page page in doc.Pages)
            {
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    Opacity = 0.2,
                    RotateAngle = 45f,                     // use RotateAngle instead of the non‑existent Rotation property
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    // The TextStamp class does not expose a Repeat property in recent versions.
                    // To achieve a tiled effect you would normally use a StampAnnotation with Repeat = true.
                    // For simplicity we place a single centered stamp per page.
                };

                // Configure visual appearance via TextState
                stamp.TextState.Font = FontRepository.FindFont("Arial");
                stamp.TextState.FontSize = 72;
                stamp.TextState.ForegroundColor = Color.FromRgb(0.8, 0.8, 0.8); // light gray

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the result; Document.Save writes PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
