using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // retained for possible future use

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

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a TextStamp that will serve as the watermark
                    TextStamp watermark = new TextStamp("CONFIDENTIAL");

                    // Set visual appearance
                    watermark.TextState.Font = FontRepository.FindFont("Arial");
                    watermark.TextState.FontSize = 72;
                    watermark.TextState.FontStyle = FontStyles.Bold;
                    watermark.TextState.ForegroundColor = Color.FromRgb(0.8, 0.8, 0.8); // light gray
                    watermark.Opacity = 0.3f; // semi‑transparent

                    // Position at the centre of the page
                    watermark.HorizontalAlignment = HorizontalAlignment.Center;
                    watermark.VerticalAlignment = VerticalAlignment.Center;

                    // Rotate the stamp (degrees)
                    watermark.RotateAngle = 45f;

                    // Add the stamp to the page content
                    page.AddStamp(watermark);
                }

                // Save the modified document as PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
