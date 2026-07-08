using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextState and FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (pages are 1‑based)
            foreach (Page page in doc.Pages)
            {
                // Create a watermark artifact
                WatermarkArtifact artifact = new WatermarkArtifact();

                // Set the watermark text
                artifact.Text = watermarkText;

                // Define text appearance (font, size, color)
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 72,
                    ForegroundColor = Aspose.Pdf.Color.Gray
                };
                artifact.TextState = textState;

                // Place the artifact behind existing page content
                artifact.IsBackground = true;

                // Make the watermark semi‑transparent
                artifact.Opacity = 0.3;

                // Position the watermark at the centre of the page.
                // WatermarkArtifact.Position expects a Point, not a Rectangle.
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;
                artifact.Position = new Aspose.Pdf.Point(pageWidth / 2, pageHeight / 2);

                // Add the artifact to the page
                page.Artifacts.Add(artifact);
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
