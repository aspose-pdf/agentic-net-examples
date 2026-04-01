using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF from a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(inputStream))
        {
            // Define text appearance for the watermark
            TextState textState = new TextState();
            textState.FontSize = 48f; // custom font size
            textState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.8f, 0.1f, 0.1f); // custom color (red shade)

            // Multi‑line watermark text
            string watermarkText = "CONFIDENTIAL\nDo Not Distribute";

            // Apply the watermark to each page
            foreach (Page page in doc.Pages)
            {
                WatermarkArtifact artifact = new WatermarkArtifact();
                artifact.SetTextAndState(watermarkText, textState);
                artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                artifact.ArtifactVerticalAlignment = VerticalAlignment.Center;
                artifact.Rotation = 45f;
                artifact.Opacity = 0.5f;
                artifact.IsBackground = true;
                page.Artifacts.Add(artifact);
            }

            // Save the watermarked PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}