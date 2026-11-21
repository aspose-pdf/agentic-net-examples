using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, TextState, WatermarkArtifact

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

        // Load the PDF document using the high‑level API (Document).
        Document doc = new Document(inputPath);

        // Common watermark appearance settings.
        Aspose.Pdf.Color wmColor = Aspose.Pdf.Color.FromRgb(0.5, 0.5, 0.5); // light gray
        float opacity = 0.3f;   // semi‑transparent
        int rotation = 45;      // degrees
        bool isBackground = false; // draw on top of existing content

        // Font to be used for all lines (size will be set per line via TextState).
        Font baseFont = FontRepository.FindFont("Arial");

        // Iterate over every page and add three watermark artifacts – one per line.
        foreach (Page page in doc.Pages)
        {
            // ----- First line (largest font) -----
            var artifactLarge = new WatermarkArtifact();
            artifactLarge.SetTextAndState(
                "CONFIDENTIAL",
                new TextState { Font = baseFont, FontSize = 48, ForegroundColor = wmColor }
            );
            artifactLarge.Rotation = rotation;
            artifactLarge.Opacity = opacity;
            artifactLarge.IsBackground = isBackground;
            artifactLarge.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
            artifactLarge.ArtifactVerticalAlignment = VerticalAlignment.Center;
            page.Artifacts.Add(artifactLarge);

            // ----- Second line (medium font) -----
            var artifactMedium = new WatermarkArtifact();
            artifactMedium.SetTextAndState(
                "Do Not Distribute",
                new TextState { Font = baseFont, FontSize = 36, ForegroundColor = wmColor }
            );
            artifactMedium.Rotation = rotation;
            artifactMedium.Opacity = opacity;
            artifactMedium.IsBackground = isBackground;
            artifactMedium.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
            artifactMedium.ArtifactVerticalAlignment = VerticalAlignment.Center;
            // Position offset – 50 points lower than the centre.
            artifactMedium.Position = new Aspose.Pdf.Point(page.PageInfo.Width / 2, (page.PageInfo.Height / 2) - 50);
            page.Artifacts.Add(artifactMedium);

            // ----- Third line (small font) -----
            var artifactSmall = new WatermarkArtifact();
            artifactSmall.SetTextAndState(
                "Internal Use Only",
                new TextState { Font = baseFont, FontSize = 24, ForegroundColor = wmColor }
            );
            artifactSmall.Rotation = rotation;
            artifactSmall.Opacity = opacity;
            artifactSmall.IsBackground = isBackground;
            artifactSmall.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
            artifactSmall.ArtifactVerticalAlignment = VerticalAlignment.Center;
            // Position offset – 100 points lower than the centre.
            artifactSmall.Position = new Aspose.Pdf.Point(page.PageInfo.Width / 2, (page.PageInfo.Height / 2) - 100);
            page.Artifacts.Add(artifactSmall);
        }

        // Save the watermarked PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
