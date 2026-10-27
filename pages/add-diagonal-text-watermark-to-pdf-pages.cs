using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermark = "CONFIDENTIAL";

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
                Page page = doc.Pages[i];

                // Create a diagonal text stamp
                TextStamp stamp = new TextStamp(watermark)
                {
                    // Rotation angle for diagonal appearance
                    RotateAngle = 45f,
                    // Place the stamp behind page content
                    Background = true,
                    // Semi‑transparent so underlying content remains readable
                    Opacity = 0.3f
                };

                // Configure text appearance
                stamp.TextState.Font = FontRepository.FindFont("Arial");
                stamp.TextState.FontSize = 72;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Color.FromRgb(0.5, 0.5, 0.5);

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
