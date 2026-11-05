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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare the text state for the watermark – this will be used in a WatermarkArtifact
            TextState textState = new TextState("Helvetica", 72);
            textState.FontStyle = FontStyles.Bold;
            // Use a light gray color; opacity will be set on the artifact itself
            textState.ForegroundColor = Color.FromRgb(0.8, 0.8, 0.8);

            // Create a watermark artifact (acts as an optional‑content layer)
            WatermarkArtifact watermark = new WatermarkArtifact();
            watermark.SetTextAndState("CONFIDENTIAL", textState);
            // Set the desired transparency (0 = fully transparent, 1 = opaque)
            watermark.Opacity = 0.3f;
            // Position can be adjusted via the artifact's properties if needed (e.g., SetPosition)

            // Add the watermark artifact to each page – this merges the layer with existing content
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Artifacts.Add(watermark);
            }

            // Save the resulting PDF; Document.Save writes PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
