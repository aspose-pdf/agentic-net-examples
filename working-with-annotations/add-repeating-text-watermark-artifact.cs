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

        using (Document doc = new Document(inputPath))
        {
            // Define the visual style of the watermark text
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 48,
                ForegroundColor = Color.LightGray
            };

            // Add a repeating watermark artifact to each page
            foreach (Page page in doc.Pages)
            {
                WatermarkArtifact artifact = new WatermarkArtifact();
                artifact.IsBackground = true;               // place behind page content
                artifact.Opacity = 0.3;                     // semi‑transparent
                artifact.Text = "CONFIDENTIAL";             // watermark text
                artifact.TextState = textState;             // apply style
                artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                artifact.ArtifactVerticalAlignment = VerticalAlignment.Center;

                page.Artifacts.Add(artifact);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}