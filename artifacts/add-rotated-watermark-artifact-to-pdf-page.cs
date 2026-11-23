using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const double rotationAngle = 45.0; // degrees

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            Page page = doc.Pages[3];

            // Create a watermark artifact. WatermarkArtifact has a parameter‑less constructor; its visual
            // appearance is defined through Text, TextState, Rotation, Opacity, and alignment properties.
            var watermark = new WatermarkArtifact
            {
                Text = "Sample Watermark",
                TextState = new TextState
                {
                    Font = FontRepository.FindFont("Arial"),
                    FontSize = 48,
                    // Use ForegroundColor (not a non‑existent Color property) to set the text colour.
                    ForegroundColor = Color.FromRgb(0.5, 0.5, 0.5)
                },
                Rotation = rotationAngle,
                Opacity = 0.3,
                // Center the watermark on the page.
                ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                ArtifactVerticalAlignment = VerticalAlignment.Center
            };

            page.Artifacts.Add(watermark);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark added to page 3 and saved as '{outputPath}'.");
    }
}
