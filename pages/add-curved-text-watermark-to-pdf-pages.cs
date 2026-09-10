using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked_curved.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages and add a series of WatermarkArtifact objects that follow a curved path.
            foreach (Page page in doc.Pages)
            {
                // Parameters for the curved path
                int steps = 7; // number of artifacts to create for a smoother curve
                double startAngle = -30; // degrees
                double endAngle = 30;    // degrees
                double radius = Math.Min(page.PageInfo.Width, page.PageInfo.Height) / 3.0;
                double centerX = page.PageInfo.Width / 2.0;
                double centerY = page.PageInfo.Height / 2.0;

                for (int i = 0; i < steps; i++)
                {
                    double t = (double)i / (steps - 1);
                    double angle = startAngle + t * (endAngle - startAngle);
                    double rad = angle * Math.PI / 180.0;

                    // Position on the circular arc
                    double posX = centerX + radius * Math.Cos(rad);
                    double posY = centerY + radius * Math.Sin(rad);

                    // Create the watermark artifact
                    WatermarkArtifact artifact = new WatermarkArtifact
                    {
                        Text = watermarkText,
                        // Position expects a Point, not a Position
                        Position = new Point(posX, posY),
                        // Rotate the text so it follows the tangent of the curve
                        Rotation = angle,
                        // Configure text appearance via TextState
                        TextState = new TextState
                        {
                            Font = FontRepository.FindFont("Helvetica"),
                            FontSize = 72,
                            // Use an ARGB color to embed transparency (alpha ~ 30%)
                            ForegroundColor = Color.FromArgb(77, 204, 0, 0), // 30% opaque red
                            BackgroundColor = Color.Transparent,
                            RenderingMode = TextRenderingMode.FillText
                        }
                    };

                    page.Artifacts.Add(artifact);
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
