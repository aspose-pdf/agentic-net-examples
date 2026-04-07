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
        const string imagePath = "logo.png";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            foreach (Page page in document.Pages)
            {
                WatermarkArtifact artifact = new WatermarkArtifact();
                // Set the image for the watermark
                artifact.SetImage(imagePath);

                // Configure the text appearance
                TextState textState = new TextState();
                textState.FontSize = 48;
                textState.Font = FontRepository.FindFont("Helvetica");
                textState.ForegroundColor = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0); // Red color

                // Set the text and its style
                artifact.SetTextAndState(watermarkText, textState);

                // Make the watermark semi‑transparent and place it behind page content
                artifact.Opacity = 0.5f;
                artifact.IsBackground = true;

                // Add the artifact to the current page
                page.Artifacts.Add(artifact);
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}