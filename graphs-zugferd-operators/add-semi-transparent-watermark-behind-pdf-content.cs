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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a watermark artifact (low‑level operator)
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Set the text and its visual style
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 72, // large size for visibility
                    ForegroundColor = Color.Gray
                };

                watermark.SetTextAndState("CONFIDENTIAL", textState);

                // Place the artifact behind page content and make it semi‑transparent
                watermark.IsBackground = true;   // behind existing content
                watermark.Opacity = 0.3;          // 30 % opacity

                // Center the watermark on the page (using page dimensions)
                double centerX = page.PageInfo.Width / 2;
                double centerY = page.PageInfo.Height / 2;
                // Position expects Aspose.Pdf.Point, not Aspose.Pdf.Text.Position
                watermark.Position = new Point(centerX, centerY);

                // Add the artifact to the page
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
