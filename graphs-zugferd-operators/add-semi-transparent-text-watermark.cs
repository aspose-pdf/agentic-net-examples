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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a semi‑transparent text stamp that will be placed behind page content
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    // Position the stamp at the center of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    // Rotate for a typical watermark effect
                    RotateAngle = 45,
                    // Make the stamp a background element (appears behind existing content)
                    Background = true,
                    // Set overall opacity (0 = fully transparent, 1 = fully opaque)
                    Opacity = 0.3
                };

                // Configure the visual appearance via the TextState object (read‑only property, but its members are mutable)
                stamp.TextState.Font = FontRepository.FindFont("Arial");
                stamp.TextState.FontSize = 72;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Apply the stamp to every page (Aspose.Pdf uses 1‑based page indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    page.AddStamp(stamp);
                }

                // Save the modified document
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