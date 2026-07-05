using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the visual style of the watermark text
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 48,
                ForegroundColor = Color.FromRgb(0.8, 0.8, 0.8) // light gray
            };

            // Create a watermark artifact that will be placed behind page contents
            WatermarkArtifact watermark = new WatermarkArtifact
            {
                IsBackground = true,               // render behind existing content
                Opacity      = 0.3,                // semi‑transparent
                TextState    = textState,          // apply the defined style
                Text         = "CONFIDENTIAL",    // text to repeat across the page
                ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                ArtifactVerticalAlignment   = VerticalAlignment.Center
            };

            // Attach the artifact to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}