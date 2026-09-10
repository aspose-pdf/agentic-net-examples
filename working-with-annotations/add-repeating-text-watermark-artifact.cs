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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the visual style of the watermark text
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 48,
                ForegroundColor = Color.FromRgb(0.8, 0.8, 0.8) // light gray
            };

            // Create a watermark artifact that will repeat across the page
            WatermarkArtifact watermark = new WatermarkArtifact
            {
                IsBackground = true,                     // place behind page content
                Opacity = 0.2,                           // semi‑transparent
                Text = "CONFIDENTIAL",                   // repeated text
                TextState = textState,                   // apply the defined style
                ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                ArtifactVerticalAlignment = VerticalAlignment.Center
            };

            // Add the artifact to every page in the document
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